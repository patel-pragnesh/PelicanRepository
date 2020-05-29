using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DellyShopApp.Managers;
using DellyShopApp.Network.Proxy.Models;
using Newtonsoft.Json;

namespace DellyShopApp.Network
{
    class DsApi
    {
        private static DsApi _instance;
        private DsHttpClient _dsHttpClient;
        private DsApi()
        {
        }

        public static void Init(string baseUrl)
        {
            if(_instance == null)
            {
                _instance = new DsApi();
            }
            _instance._dsHttpClient = new DsHttpClient(baseUrl);
        }

        public static DsApi GetInstance()
        {
            if(_instance == null)
            {
                throw new InvalidOperationException("Please call Init() at the start of the application vefore calling GetInstance()");

            }

            return _instance;
        }


        private NameValueCollection GetParams(string jsonString)
        {
            Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            NameValueCollection nvc = null;
            if (dict != null)
            {
                nvc = new NameValueCollection(dict.Count);
                foreach (var k in dict)
                {
                    nvc.Add(k.Key, k.Value.ToString());
                }
            }
            return nvc;
        }

        public async Task<List<PRXResponseProduct>> RetrieveProductInfoAsync()
        {
            List<PRXResponseProduct> domList = new List<PRXResponseProduct>();
            var result = await _dsHttpClient.GetAsync<List<PRXResponseProduct>>(Constants.DsApiEndPoints.ProductsUrl);

            if(result != null && result.ResponseBody != null && result.ResponseBody.Count >0)
            {
                foreach (PRXResponseProduct resp in result.ResponseBody)
                {
                    domList.Add(new PRXResponseProduct()
                    {
                        description = resp.description,
                        discountPercent = resp.discountPercent,
                        serialNumber = resp.serialNumber,
                        createdAt = resp.createdAt,
                        barcode = resp.barcode,
                        mrp = resp.mrp,
                        productCode = resp.productCode,
                        productId = resp.productId,
                        productImages = resp.productImages,
                        productName = resp.productName,
                        productType = resp.productType,
                        uom = resp.uom
                    });
                }
            }

            return domList;
        }

        public async Task<bool>CreateOrderAsync(PRXRequestOrder request)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var result = await _dsHttpClient.PostAsync<PRXRequestOrder>(Constants.DsApiEndPoints.CreateOrderUrl,null,httpContent);
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }


        }

    }
}
