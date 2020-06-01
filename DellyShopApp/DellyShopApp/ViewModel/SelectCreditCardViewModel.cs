using System;
using Acr.UserDialogs;
using DellyShopApp.DAL.DAO;
using DellyShopApp.Models;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class SelectCreditCardViewModel : BaseVm
    {
        INavigation Navigation;
        private Order order;
        IBasketDAO basketDAO;
        public SelectCreditCardViewModel(INavigation navigation, Order order)
        {
            this.Navigation = navigation;
            this.order = order;
            basketDAO = new BasketDAO();
        }

        public async void CreateOrder()
        {
            using(UserDialogs.Instance.Loading("Creating Order"))
            {
                try
                {
                    if (order != null)
                    {
                        var res = await basketDAO.CreateOrder(order);
                        UserDialogs.Instance.HideLoading();
                        if(res != null)
                        {
                            await basketDAO.ClearBasket();
                            await UserDialogs.Instance.AlertAsync("Success Order");
                            await Navigation.PushAsync(new SuccessPage());
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync("Something went wrong, please try again...");
                        }

                    }
                }
                catch(Exception e)
                {
                    UserDialogs.Instance.HideLoading();
                    throw e;
                }
            }
         }
    }
}
