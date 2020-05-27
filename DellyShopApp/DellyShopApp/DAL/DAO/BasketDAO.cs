using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DellyShopApp.DAL.Entity;
using DellyShopApp.Models;
using DellyShopApp.Persistance;
using Xamarin.Forms;

namespace DellyShopApp.DAL.DAO
{
    public class BasketDAO : IBasketDAO
    {
        Synchronizer synchronizer;
        ISQLLiteDb db;
        DBHandler dBHandler;
        public BasketDAO()
        {
            db = DependencyService.Get<ISQLLiteDb>();
            dBHandler = new DBHandler(db);
            synchronizer = new Synchronizer(db);
        }

        private Task<List<ENTBasketItem>> GetBasketFromRemote()
        {
            return dBHandler.GetAllBasketItems();
        }

        public async Task<bool> AddBasketItem(BasketItem basketItem)
        {
            try
            {
                if (basketItem != null)
                {
                    await dBHandler.AddProductToBasket(new ENTBasketItem()
                    {
                        ProductId = basketItem.ProductId,
                        Quantity = basketItem.Quantity
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(); //To Do: Handle Exception
            }

        }

        public async Task<List<BasketItem>> GetAllBasketItems()
        {
            try
            {
                var remoteBasket = await GetBasketFromRemote();
                var domList = new List<BasketItem>();
                if (remoteBasket != null && remoteBasket.Count > 0)
                {
                    foreach (ENTBasketItem item in remoteBasket)
                    {
                        domList.Add(new BasketItem()
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        });
                    }
                    if (domList.Count > 0)
                        return domList;
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
    
        }
    }
}
