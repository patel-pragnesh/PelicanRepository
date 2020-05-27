using System;
using System.Collections.Generic;
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
        private Task<List<ENTProduct>> GetProductsFromRemote()
        {
            return dBHandler.GetProducts();
        }
        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                List<ENTProduct> remoteProducts = await GetProductsFromRemote();
                List<Product> domList = new List<Product>();
                List<ProductImages> domImageList = new List<ProductImages>();

                if(remoteProducts != null && remoteProducts.Count>0)
                {
                    foreach(ENTProduct p in remoteProducts)
                    {
                        if(p.ProductImages != null && p.ProductImages.Count>0)
                        {
                            foreach(ENTImages img in p.ProductImages)
                            {
                                domImageList.Add(new ProductImages()
                                {
                                    ImagePath = img.ImagePath
                                });
                            }
            
                        }
                        domList.Add(new Product()
                        {
                            UOM = p.UOM,
                            SerialNumber = p.SerialNumber,
                            BarCode = p.BarCode,
                            CreatedAt = p.CreatedAt,
                            Description = p.Description,
                            DiscountPercent = p.DiscountPercent,
                            Id = p.Id,
                            MRP = p.MRP,
                            ProductCode = p.ProductCode,
                            ProductId = p.ProductId,
                            ProductImages = domImageList,
                            ProductName = p.ProductName,
                            ProductType = p.ProductType
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

        #endregion

        #region Save All Products
        public async Task<bool> SaveProducts(List<Product> products)
        {
            try
            {
                if(products != null)
                {
                    await synchronizer.SyncProductAsync(new List<ENTProduct>()
                    {
                        // set attr

                    });
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                throw new Exception(); //To Do: Handle Exception
            }
        }
        #endregion
    }
}
