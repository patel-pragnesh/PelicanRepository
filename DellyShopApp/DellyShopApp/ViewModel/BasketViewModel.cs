using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DellyShopApp.DAL.DAO;
using DellyShopApp.Models;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;
using Xamarin.Forms.Internals;

namespace DellyShopApp.ViewModel
{
    public class BasketViewModel : BasketVm
    {
        IBasketDAO basketDAO;
        private ObservableCollection<Product> _productsInBasket;
        private string _totalCost;
        public string TotalCost
        {
            get
            {
                if(ProductsInBasket != null && ProductsInBasket.Count >0)
                {
                    double totalCost = (from x in ProductsInBasket select x.MRP).Sum();
                    return totalCost.ToString();
                }
                return "0";
      
            }
        }
        private List<BasketItem> _basketList;
        public ObservableCollection<Product> ProductsInBasket
        {
            get
            {
                return _productsInBasket;
            }
            set
            {
                SetProperty(ref _productsInBasket, value);
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public BasketViewModel()
        {
            basketDAO = new BasketDAO();
            //LoadData();
        }

        public Order GetOrderList()
        {
            var order = new Order();
            var itemList = new List<Item>();
            if (ProductsInBasket != null && ProductsInBasket.Count>0 && _basketList != null && _basketList.Count>0)
            {
                foreach (BasketItem item in _basketList)
                {
                    var relevantProduct = ProductsInBasket.Where(p => p.ProductId == item.ProductId).FirstOrDefault();
                    if (relevantProduct != null)
                    {
                        itemList.Add(new Item()
                        {
                            Price = relevantProduct.MRP,
                            DiscountAmount = 0,
                            ProductId = relevantProduct.ProductId,
                            Qty = item.Quantity
                        });
                    }
                }
                order.BranchId = "939fb8df-d9b9-42ff-a6b4-41f281059277";
                order.CustomerId = AuthenticatedUser.LoggedInUser.CustomerId;
                order.DeliveryAddress = "Galle";
                order.Description = "Testing Visal";
                order.Items = itemList;
                order.PicCustomer = "PC001";
                order.PicInternal = "PC002";
                order.Top = 10;
                order.ReferenceNumberExternal = "RC00001";
                order.ReferenceNumberInternal = "RN00001";

                return order;
            }
            return null;

        }

        private async Task LoadBasketItems()
        {
            
            using (UserDialogs.Instance.Loading("Loading your Basket"))
            {
                try
                {
                    var items = await basketDAO.GetAllBasketItems();
                    _basketList = items;
                    UserDialogs.Instance.HideLoading();
                }
                catch(Exception e)
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        public async void LoadData()
        {
            await LoadBasketItems();
            await LoadProductsData();
        }

        private async Task LoadProductsData()
        {
            using (UserDialogs.Instance.Loading("Loading Products..."))
            {
                try
                {
                    var data = new ObservableCollection<Product>();
                    var result = await DsApi.GetInstance().RetrieveProductInfoAsync();
                    if (result != null && result.Count > 0)
                    {
                        foreach (PRXResponseProduct product in result)
                        {
                            if(_basketList != null && _basketList.Count > 0)
                            {
                                foreach (BasketItem item in _basketList)
                                {
                                    if(item.ProductId == product.productId)
                                    {
                                        List<ProductImages> imgs = new List<ProductImages>();
                                        if(product.productImages != null && product.productImages.Count>0)
                                        {
                                            foreach(PRXImages img in product.productImages)
                                            {
                                                imgs.Add(new ProductImages()
                                                {
                                                    ImagePath = img.imagePath
                                                });
                                            }
                                        }
                                        for(int i=0; i<item.Quantity; i++)
                                        {
                                            data.Add(new Product()
                                            {
                                                ProductName = product.productName,
                                                ProductImages = imgs,
                                                MRP = product.mrp,
                                                ProductId = product.productId,

                                            });
                                        }
                                    }
                                
                                }
    
                            }

                        }

                    }
                    ProductsInBasket = data;
                    UserDialogs.Instance.HideLoading();
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.HideLoading();
                    throw e;
                }
            }

        }
    }
}
