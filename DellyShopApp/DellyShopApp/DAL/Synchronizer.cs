using System;
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

        public async Task ClearProduct()
        {
            await Handler.ClearProducts();
        }

        public async Task SyncProductAsync(ENTProduct product)
        {
            await Handler.ClearProducts();
            await Handler.SaveProduct(product);
        }
    }
}
