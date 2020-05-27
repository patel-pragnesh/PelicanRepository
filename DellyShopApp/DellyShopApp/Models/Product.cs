using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string SerialNumber { get; set; }
        public double MRP { get; set; }
        public double DiscountPercent { get; set; }
        public int UOM { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<ProductImages> ProductImages { get; set; }
    }

    public class ProductImages
    {
        public string ImagePath { get; set; }
    }
}
