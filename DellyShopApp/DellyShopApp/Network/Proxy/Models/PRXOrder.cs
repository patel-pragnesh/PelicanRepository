using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DellyShopApp.Network.Proxy.Models
{
    public class PRXRequestCreateOrder
    {
        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("referenceNumberInternal")]
        public string ReferenceNumberInternal { get; set; }

        [JsonProperty("referenceNumberExternal")]
        public string ReferenceNumberExternal { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("branchId")]
        public string BranchId { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("picInternal")]
        public string PicInternal { get; set; }

        [JsonProperty("picCustomer")]
        public string PicCustomer { get; set; }

        [JsonProperty("items")]
        public IList<PRXCreateOrderItem> Items { get; set; }


    }

    public class PRXCreateOrderItem
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("discountAmount")]
        public double DiscountAmount { get; set; }

        [JsonProperty("totalAmount")]
        public double TotalAmount { get; set; }

    }

    public class PRXResponseCreateOrder
    {
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("picCustomer")]
        public string PicCustomer { get; set; }

        [JsonProperty("referenceNumberExternal")]
        public string ReferenceNumberExternal { get; set; }

        [JsonProperty("totalOrderAmount")]
        public double TotalOrderAmount { get; set; }

        [JsonProperty("TotalDiscountAmount")]
        public double TotalDiscountAmount { get; set; }
    }

}
