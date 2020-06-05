using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DellyShopApp.Network.Proxy.Models
{
    public class PRXOrdersByCustomerRequest
    {

    }

    public class PRXOrdersByCustomerResponse
    {
        [JsonProperty("salesOrderId")]
        public string SalesOrderId { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("orderStatus")]
        public int OrderStatus { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("picCustomer")]
        public string PicCustomer { get; set; }

        [JsonProperty("referenceNumberExternal")]
        public string ReferenceNumberExternal { get; set; }

        [JsonProperty("totalOrderAmount")]
        public double TotalOrderAmount { get; set; }

        [JsonProperty("totalDiscountAmount")]
        public double TotalDiscountAmount { get; set; }

        [JsonProperty("orderedDate")]
        public DateTime OrderedDate { get; set; }

        [JsonProperty("items")]
        public IList<PRXOrderByCustomerItem> Items { get; set; }
    }

    public class PRXOrderByCustomerItem
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("discountAmount")]
        public double DiscountAmount { get; set; }
    }

  
}
