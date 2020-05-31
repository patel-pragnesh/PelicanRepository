using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DellyShopApp.DAL.Entity;
using DellyShopApp.Models;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;
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

        public async Task<PRXResponseCreateOrder> CreateOrder(Order orders)
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<PRXRequestOrder, Order>());
            //var mapper = new Mapper(config);
            //PRXRequestOrder request = mapper.Map<PRXRequestOrder>(orders);
            PRXRequestCreateOrder request = null;
            try
            {
                List<PRXCreateOrderItem> ps = new List<PRXCreateOrderItem>();

                if (orders != null && orders.Items != null && orders.Items.Count > 0)
                {
                    foreach(Item it in orders.Items)
                    {
                        ps.Add(new PRXCreateOrderItem()
                        {
                            DiscountAmount = it.DiscountAmount,
                            Price = it.Price,
                            ProductId = it.ProductId,
                            Qty = it.Qty,
                            TotalAmount = it.Price
                        });
                    }
                    request = new PRXRequestCreateOrder()
                    {
                        BranchId = orders.BranchId,
                        CustomerId = AuthenticatedUser.LoggedInUser.CustomerId,
                        DeliveryAddress = orders.DeliveryAddress,
                        Description = orders.Description,
                        Items = ps,
                        PicCustomer = orders.PicCustomer,
                        PicInternal = orders.PicInternal,
                        ReferenceNumberExternal = orders.ReferenceNumberExternal,
                        ReferenceNumberInternal = orders.ReferenceNumberInternal,
                        Top = orders.Top
                    };
               
                }

                if(request != null)
                {
                    return await DsApi.GetInstance().CreateOrderAsync(request);
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
