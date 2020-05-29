using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class Order
    {
        public int Top { get; set; }

        public string DeliveryAddress { get; set; }

        public string ReferenceNumberInternal { get; set; }

        public string ReferenceNumberExternal { get; set; }

        public string Description { get; set; }

        public string BranchId { get; set; }

        public string CustomerId { get; set; }

        public string PicInternal { get; set; }

        public string PicCustomer { get; set; }

        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string ProductId { get; set; }

        public int Qty { get; set; }

        public double Price { get; set; }

        public double DiscountAmount { get; set; }

        public double TotalAmount { get; set; }
    }
}
