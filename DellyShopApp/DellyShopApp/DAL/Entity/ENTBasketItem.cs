using System;
using SQLite;

namespace DellyShopApp.DAL.Entity
{
    public class ENTBasketItem
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
