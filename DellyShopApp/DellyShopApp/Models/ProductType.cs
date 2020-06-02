using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class ProductType
    {
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeImage { get; set; }
        public List<Product> Products { get; set; }
    }
}
