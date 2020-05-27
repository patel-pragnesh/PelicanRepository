using System;
using System.Threading.Tasks;
using DellyShopApp.DAL.Entity;
using DellyShopApp.Models;
using DellyShopApp.Persistance;
using Xamarin.Forms;

namespace DellyShopApp.DAL.DAO
{
    public class ProductDAO: IProductDAO
    {
        Synchronizer synchronizer;
        ISQLLiteDb db;
        DBHandler dBHandler;
        public ProductDAO()
        {
            db = DependencyService.Get<ISQLLiteDb>();
            dBHandler = new DBHandler(db);
            synchronizer = new Synchronizer(db);
        }


        #region Get Product
        private Task<ENTProduct> GetProductFromRemote()
        {
            return dBHandler.GetProduct();
        }
        public async Task<Product> GetProduct()
        {
            try
            {
                ENTProduct remoteProduct = await GetProductFromRemote();
                if(remoteProduct != null)
                {
                    return new Product()
                    {
                        //set attr
                    };
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Update Product
        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                if(product != null)
                {
                    await synchronizer.SyncProductAsync(new ENTProduct()
                    {
                        // set attr

                    });
                    return product;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(); //To Do: Handle Exception
            }
        }
        #endregion
    }
}
