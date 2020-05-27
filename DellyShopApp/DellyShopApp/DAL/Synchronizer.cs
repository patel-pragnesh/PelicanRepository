using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DellyShopApp.DAL.Entity;
using DellyShopApp.Persistance;

namespace DellyShopApp.DAL
{
    public class Synchronizer
    {
        DBHandler Handler;
        public Synchronizer(ISQLLiteDb db)
        {
            Handler = new DBHandler(db);
        }

        #region Product
        public async Task ClearAllProduct()
        {
            await Handler.ClearProducts();
        }

        public async Task SyncProductAsync(List<ENTProduct> products)
        {
            await Handler.ClearProducts();
            await Handler.SaveProducts(products);
        }
        #endregion

        #region Basket
        public async Task ClearAllBasketItems()
        {
            await Handler.ClearBasket();
        }

        public async Task SyncBasketItems(List<ENTBasketItem> basketItems)
        {
            await Handler.ClearProducts();
            //await Handler.SaveProducts(products);
        }
        #endregion
    }
}
