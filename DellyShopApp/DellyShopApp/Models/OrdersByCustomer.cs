using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class OrdersByCustomer
    {
        public string SalesOrderId { get; set; }

        public string OrderNumber { get; set; }

        public int OrderStatus { get; set; }

        public string Branch { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string PicCustomer { get; set; }

        public string ReferenceNumberExternal { get; set; }

        public double TotalOrderAmount { get; set; }

        public double TotalDiscountAmount { get; set; }

        public DateTime OrderedDate { get; set; }

        public List<OrderByCustomerItem> Items { get; set; }

    }

    public class OrderByCustomerItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public double Qty { get; set; }

        public double Price { get; set; }

        public double TotalAmount { get; set; }

        public double DiscountAmount { get; set; }

        //Added to match view
        public string OrderNumber { get; set; }

        public int OrderStatus { get; set; }

        public DateTime OrderedDate { get; set; }
    }
}
