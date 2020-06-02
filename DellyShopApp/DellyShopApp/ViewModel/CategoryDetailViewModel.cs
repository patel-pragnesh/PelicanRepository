using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DellyShopApp.Models;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class CategoryDetailViewModel : BaseVm
    {
        private INavigation Navigation;
        private ProductType _productType;
        public ProductType ProductType
        {
            get
            {
                return _productType;
            }
            set
            {
                SetProperty(ref _productType, value);
                OnPropertyChanged(nameof(ProductType));
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

        public CategoryDetailViewModel(INavigation navigation, ProductType productType)
        {
            this.Navigation = navigation;
            this.ProductType = productType;
            LoadProducts(productType.Products);
        }

        private void LoadProducts(List<Product> products)
        {
            var data = new ObservableCollection<Product>();
            foreach(Product p in products)
            {
                data.Add(p);
            }
            Products = data;
        }
    }
}
