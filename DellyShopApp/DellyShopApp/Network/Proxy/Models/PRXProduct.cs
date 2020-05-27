using System;
using System.Collections.Generic;

namespace DellyShopApp.Network.Proxy.Models
{
    public class PRXRequestProduct
    {

    }
    public class PRXResponseProduct
    {
        public string productId { get; set; }
        public string barcode { get; set; }
        public string description { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int productType { get; set; }
        public string serialNumber { get; set; }
        public double mrp { get; set; }
        public double discountPercent { get; set; }
        public int uom { get; set; }
        public DateTime createdAt { get; set; }
        public IList<PRXImages> productImages { get; set; }

    }

    public class PRXImages
    {
        public string imagePath { get; set; }
    }
}
