using System;
namespace DellyShopApp.Managers
{
    public class Constants
    {
        public const int AuthTokenRetryLimit = 1;
        public Constants()
        { 

        }

        public class DsApiEndPoints
        {
            public const string OAuthTokenUrl = "api/oauth/token";
            public const string CreateOrderUrl = "api/order/create";
            public const string ProductsUrl = "api/products";
        }


        public class DsOauthConstants
        {
            public const string AuthorizationToken = "";

            public const string KeyGrantType = "grant_type";
            public const string ValueGrantType = "password";

            public const string KeyClientId = "client_id";
            public const string ValueClientId = "my-trusted-client";

            public const string KeyClientSecret = "client_secret";
            public const string ValueClientSecret = "secret";

            public const string KeyUsername = "username";
            public const string ValueUsername = "dsuser";

            public const string KeyPassword = "password";
            public const string ValuePassword = "";
        }
    }
}
