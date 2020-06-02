using System;
using System.Collections.Generic;
using SQLite;

namespace DellyShopApp.DAL.Entity
{
    public class ENTProduct
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string ProductId { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public double MRP { get; set; }
        public double Stock { get; set; }
        public double DiscountPercent { get; set; }
        public int UOM { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<ENTImages> ProductImages { get; set; }
    }

    public class ENTImages
    {
        public string ImagePath { get; set; }
    }
}
