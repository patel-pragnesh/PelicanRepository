using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using DellyShopApp.Models;
using DellyShopApp.Network;
using DellyShopApp.Network.Proxy.Models;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class HomeViewModel : BaseVm
    {
        private ObservableCollection<ProductType> _productTypes;
        public ObservableCollection<ProductType> ProductTypes
        {
            get
            {
                return _productTypes;
            }
            set
            {
                SetProperty(ref _productTypes, value);
                OnPropertyChanged(nameof(ProductTypes));
            }
        }
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
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
                    var data = new ObservableCollection<Product>();
                    var result = await DsApi.GetInstance().RetrieveProductInfoAsync();
                    LoadProductTypesData(result);
                    if (result != null && result.Count > 0)
                    {
                        foreach (PRXResponseProduct prod in result)
                        {
                            var images = new List<ProductImages>();
                            if (prod.productImages != null && prod.productImages.Count > 0)
                            {
                                foreach (var img in prod.productImages)
                                {
                                    images.Add(new ProductImages()
                                    {
                                        ImagePath = img.imagePath
                                    });
                                }
                            }
                            data.Add(new Product()
                            {
                                BarCode = prod.barcode,
                                CreatedAt = prod.createdAt,
                                Description = prod.description,
                                DiscountPercent = prod.discountPercent,
                                MRP = prod.mrp,
                                UOM = prod.uom,
                                ProductCode = prod.productCode,
                                ProductId = prod.productId,
                                SerialNumber = prod.serialNumber,
                                ProductTypeName = prod.productTypeName,
                                ProductTypeId = prod.productTypeId,
                                ProductImages = images,
                                ProductName = prod.productName

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

        private void LoadProductTypesData(List<PRXResponseProduct> products)
        {

            var groupedList = products.GroupBy(s => s.productTypeId).ToList();
            var list = new ObservableCollection<ProductType>();
            var carsByPersonId = products.ToLookup(p => p.productTypeId);
            foreach (var key in groupedList)
            {
                var items = new List<Product>();
                string prodTypeName = "",productTypeId = "";
                foreach(PRXResponseProduct prod in key)
                {
                    prodTypeName = prod.productTypeName;
                    productTypeId = prod.productTypeId;
                    var images = new List<ProductImages>();
                    if(prod.productImages != null && prod.productImages.Count > 0)
                    {
                        foreach(var img in prod.productImages)
                        {
                            images.Add(new ProductImages()
                            {
                                ImagePath = img.imagePath
                            });
                        }
                    }
                    items.Add(new Product()
                    {
                        BarCode = prod.barcode,
                        CreatedAt = prod.createdAt,
                        Description = prod.description,
                        DiscountPercent = prod.discountPercent,
                        MRP = prod.mrp,
                        UOM = prod.uom,
                        ProductCode = prod.productCode,
                        ProductId = prod.productId,
                        SerialNumber = prod.serialNumber,
                        ProductTypeName = prod.productTypeName,
                        ProductTypeId = prod.productTypeId,
                        ProductImages = images,
                        ProductName = prod.productName
                    });
                }
                list.Add(new ProductType()
                {
                    Products = items,
                    ProductTypeId = productTypeId,
                    ProductTypeName = prodTypeName,
                    ProductTypeImage = "product_type.png"

                });
            }
            ProductTypes = list;
        }
    }
}
