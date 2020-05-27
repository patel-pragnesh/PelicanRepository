using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DellyShopApp.Managers;
using DellyShopApp.Network.Proxy.Models;
using Newtonsoft.Json;

namespace DellyShopApp.Network
{
    internal class DsHttpClient
    {
        private HttpClient _httpClient;
        private string _baseUrl;
        private OAuthAccessTokenResponse _tokenResponse;

        internal DsHttpClient(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        private async Task<HttpClient> GetHttpClient()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }

            if(_tokenResponse == null)
            {
                OAuthAccessTokenResponse authToken = await RetrieveAuthToken();

                if(authToken == null)
                {
                    //return null;
                    authToken = new OAuthAccessTokenResponse() { AccessToken = "dummy-access-token"};
                }
                else
                {
                    _tokenResponse = authToken;
                }
            }

            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenResponse.AccessToken);
            return _httpClient;
        }

        private async Task<OAuthAccessTokenResponse> RetrieveAuthToken()
        {
            OAuthAccessTokenResponse authToken = null;
            Uri uri = new Uri(new Uri(_baseUrl), Constants.DsApiEndPoints.OAuthTokenUrl);

            MultipartFormDataContent httpContent = new MultipartFormDataContent
            {
                { new StringContent(Constants.DsOauthConstants.ValueGrantType),"\"" + Constants.DsOauthConstants.KeyGrantType + "\"" },
                { new StringContent(Constants.DsOauthConstants.ValueClientId),"\"" + Constants.DsOauthConstants.KeyClientId + "\"" },
                { new StringContent(Constants.DsOauthConstants.ValueClientSecret),"\"" + Constants.DsOauthConstants.KeyClientSecret + "\"" },
                { new StringContent(Constants.DsOauthConstants.ValueUsername),"\"" + Constants.DsOauthConstants.KeyUsername + "\"" },
                { new StringContent(Constants.DsOauthConstants.ValuePassword),"\"" + Constants.DsOauthConstants.KeyPassword + "\"" },
            };

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Constants.DsOauthConstants.AuthorizationToken);
            HttpResponseMessage response = await _httpClient.PostAsync(uri, httpContent);

            if(response.IsSuccessStatusCode)
            {
                string responseStr = await response.Content.ReadAsStringAsync();
                authToken = JsonConvert.DeserializeObject<OAuthAccessTokenResponse>(responseStr);
            }
            return authToken;
        }

        public async Task<Response<T>> PostAsync<T>(string endPoint, NameValueCollection parameters = null, HttpContent httpContent = null)
        {
            HttpRequest request = new HttpRequest(new Uri(new Uri(_baseUrl), endPoint), HttpRequestType.POST, httpContent, parameters);
            return await ResolveResponse<T>(request);

        }

        public async Task<Response<T>> GetAsync<T>(string endPoint, NameValueCollection parameters = null, params string[] pathComponents)
        {
            foreach (string pathComponent in pathComponents)
            {
                endPoint += "/" + pathComponent;
            }

            HttpRequest request = new HttpRequest(new Uri(new Uri(_baseUrl), endPoint), HttpRequestType.GET, null, parameters);
            return await ResolveResponse<T>(request);
        }


        private async Task<Response<T>> ResolveResponse<T>(HttpRequest request)
        {
            Response<T> result;

            HttpClient httpClient = await GetHttpClient();

            //check if retrieving token was successfull
            if(httpClient == null)
            {
                return new Response<T>(Enum.ResponseError.AuthorizationError);
            }

            UriBuilder uriBuilder = new UriBuilder(request.Uri)
            {
                Port = -1
            };

            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add(request.Parameters);

            uriBuilder.Query = query.ToString();

            HttpResponseMessage response;
            if(request.HttpRequestType == HttpRequestType.GET)
            {
                response = await httpClient.GetAsync(uriBuilder.Uri);
            }
            else
            {
                response = await httpClient.PostAsync(uriBuilder.Uri, request.HttpContent);
            }

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                try
                {
                    T saveContactKeyResponse = JsonConvert.DeserializeObject<T>(content);
                    result = new Response<T>(saveContactKeyResponse);
                }
                catch(JsonReaderException e)
                {
                    result = new Response<T>(Enum.ResponseError.ContentError, e);
                }
            }
            else
            {
                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    OAuthAccessTokenResponse authToken = await RetrieveAuthToken();

                    if(authToken == null)
                    {
                        result = new Response<T>(Enum.ResponseError.AuthorizationError);
                    }
                    else
                    {
                        _tokenResponse = authToken;
                        if(request.retryCount < Constants.AuthTokenRetryLimit)
                        {
                            request.retryCount++;
                            result = await ResolveResponse<T>(request);
                        }
                        else
                        {
                            result = new Response<T>(Enum.ResponseError.AuthorizationError);
                        }
                    }
                }
                else
                {
                    result = new Response<T>(Enum.ResponseError.ResponseError);
                }
            }
            return result;
        }
        
    }

    class HttpRequest
    {
        internal Uri Uri { get; }
        internal HttpRequestType HttpRequestType { get; }
        internal HttpContent HttpContent { get; }
        internal NameValueCollection Parameters { get; }
        internal int retryCount;

        public HttpRequest(Uri uri,HttpRequestType httpRequestType, HttpContent httpContent = null,NameValueCollection parameters = null)
        {
            Uri = uri;
            HttpRequestType = httpRequestType;
            HttpContent = httpContent;
            if(parameters == null)
            {
                parameters = new NameValueCollection();
            }
            Parameters = parameters;
        }
    }

    enum HttpRequestType
    {
        GET,
        POST
    }
}
