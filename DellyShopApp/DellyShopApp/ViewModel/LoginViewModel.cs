using System;
using Acr.UserDialogs;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class LoginViewModel : BaseVm
    {
        #region Properties
        private string _email, _password;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                SetProperty(ref _email, value);
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion
        INavigation Navigation;
        public LoginViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        public async void CustomerLogin()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Loging user. please wait..."))
                {
                    var req = new PRXRequestCustomerLogin
                    {
                        Password = Password,
                        Email = Email
                    };
                    var res = await DsApi.GetInstance().CustomerLogin(req);

                    if (res != null && res.CustomerId != null)
                    {
                        UserDialogs.Instance.HideLoading();
                        await Navigation.PushAsync(new HomeTabbedPage());
                    }
                }

            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Incorrect email or password. Please try again");
            }
        }
    }
}
