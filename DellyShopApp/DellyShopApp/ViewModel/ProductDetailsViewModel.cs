using System;
using Acr.UserDialogs;
using DellyShopApp.DAL.DAO;
using DellyShopApp.Models;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class ProductDetailsViewModel : BaseVm
    {
        IBasketDAO basketDAO;
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                SetProperty(ref _product, value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public ProductDetailsViewModel(Product product,INavigation navigation)
        {
            basketDAO = new BasketDAO();
            Product = product;
        }

        public async void AddToBasket(int quantity)
        {
            try
            {
                using(UserDialogs.Instance.Loading("Adding to Basket"))
                {
                    BasketItem item = new BasketItem()
                    {
                        ProductId = Product.ProductId,
                        Quantity = quantity
                    };
                    await basketDAO.AddBasketItem(item);
                }
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Items Added to Basket Successfully");
        
            }
            catch(Exception)
            {
                UserDialogs.Instance.HideLoading();
            }

        }
    }
}
