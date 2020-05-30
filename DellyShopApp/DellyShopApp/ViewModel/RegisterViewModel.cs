using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class RegisterViewModel : BaseVm
    {
        #region Properties
        private string _firstName, _lastName, _email, _phoneNumber, _password, _confirmPassword, _address;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                SetProperty(ref _firstName, value);
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                SetProperty(ref _lastName, value);
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                SetProperty(ref _phoneNumber, value);
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

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

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                SetProperty(ref _confirmPassword, value);
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                SetProperty(ref _address, value);
                OnPropertyChanged(nameof(Address));
            }
        }

        #endregion
        private INavigation Navigation;
        public RegisterViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        public async void RegisterCustomer()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Creating user. please wait..."))
                {
                    var req = new PRXRequestCustomerRegister
                    {
                        CustomerName = FirstName + LastName,
                        Email = Email,
                        Password = Password,
                        City = Address,
                        Size = 1,
                        Country = "Sri Lanka",
                        Street1 = "123/B",
                        Street2 = "Rawthawathha",
                        Description = "",
                        Province = "Southern",
                        Contacts = new List<PRXCustomerRegisterContact>()
                        {
                            new PRXCustomerRegisterContact(){
                                FirstName = FirstName,
                                Fax = "1232343452",
                                Gender = 1,
                                JobTitle = "testJobTitle",
                                LastName = LastName,
                                MiddleName = "testMiddleName",
                                MobilePhone = "1232342341",
                                NickName = "testNickName",
                                OfficePhone = "1231231233",
                                PersonalEmail = "r@live.com",
                                Salutation = 1,
                                WorkEmail = "j@l.com"
                            }
                        }
                    };
                    var res = await DsApi.GetInstance().CustomerRegister(req);

                    if(res != null && res.CustomerId != null)
                    {
                        UserDialogs.Instance.HideLoading();
                        await Navigation.PushAsync(new HomeTabbedPage());
                    }
                }
            
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
