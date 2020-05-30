using System;
using Newtonsoft.Json;

namespace DellyShopApp.Network.Proxy.Models
{
    public class PRXRequestCustomerLogin
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class PRXResponseCustomerLogin
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
    }
}
