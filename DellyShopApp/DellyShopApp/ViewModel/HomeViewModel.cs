using System;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using DellyShopApp.Models;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;

namespace DellyShopApp.ViewModel
{
    public class HomeViewModel : BaseVm
    {
        private ObservableCollection<ProductListModel> _products;
        public ObservableCollection<ProductListModel> Products
        {
            get
            {
                return _products;
            }
            set
            {
                SetProperty(ref _products, value);
                OnPropertyChanged(nameof(Products));
            }
        }
        public HomeViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            using (UserDialogs.Instance.Loading("Loading..."))
            {
                try
                {
                    var data = new ObservableCollection<ProductListModel>();
                    var result = await DsApi.GetInstance().RetrieveProductInfoAsync();
                    if (result != null && result.Count > 0)
                    {
                        foreach (PRXResponseProduct product in result)
                        {
                            data.Add(new ProductListModel()
                            {
                                Image = product.productImages.Count > 0 ? product.productImages[0].imagePath : "",
                                Price = product.mrp,
                                ProductId = product.productId,
                                Title = product.productName,
                                Brand =  "",

                            });
                        }

                        Products = data;
                    }
                    UserDialogs.Instance.HideLoading();
                }
                catch(Exception e)
                {
                    UserDialogs.Instance.HideLoading();
                }
            }

        }
    }
}
