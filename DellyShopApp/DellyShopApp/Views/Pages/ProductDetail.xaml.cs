using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail
    {
        int productCount;
        private readonly List<StartList> _startList = new List<StartList>();
        private readonly List<CommentModel> _comments = new List<CommentModel>();
        private readonly ProductListModel _products;
        public ProductDetail(ProductListModel product)
        {
            _products = product;

            _startList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            _startList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            _startList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            _startList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            _startList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            _comments.Add(new CommentModel
            {
                Name = "Saman Perera",
                CommentTime = "12/1/20",
                Id = 1,
                Rates = _startList
            });
            _comments.Add(new CommentModel
            {
                Name = "John Goldman",
                CommentTime = "11/1/20",
                Id = 2,
                Rates = _startList.Skip(0).ToList()
            });
            InitializeComponent();
            //this.BindingContext = product;
            starList.ItemsSource = _startList;
            starListglobal.ItemsSource = _startList;

            CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled;

            this.BindingContext = new ProductDetailsViewModel(product,Navigation);


        }

        ProductDetailsViewModel ViewModel
        {
            get
            {
                return (ProductDetailsViewModel)BindingContext;
            }

        }
        private void PlusClick(object sender, EventArgs e)
        {
            if (productCount >= 10) return;
            ProductCountLabel.Text = (++productCount).ToString();
        }
        private void MinusClick(object sender, EventArgs e)
        {
            if (productCount == 0) return;
            ProductCountLabel.Text = (--productCount).ToString();
        }
        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            var height = Math.Round(Application.Current.MainPage.Height);
            var ycordinate = Math.Round(e.ScrollY);
            if (ycordinate > (height / 3))
            {
                NavbarStack.IsVisible = true;
                return;
            }

            NavbarStack.IsVisible = false;
        }

        private async void CommentsPageClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentsPage(_products));
        }

        async void AddBasketButton(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Add this to basket?",
                OkText = "Add",
                CancelText = "Cancel"
            });
            if (result)
            {
                if (productCount == 0)
                {
                    productCount = 1;
                }
                    
                ViewModel.AddToBasket(productCount);
            }
                //DisplayAlert(TranslateExtension.Translate("Success"), _products.Title+" "+TranslateExtension.Translate("AddedBakset"),TranslateExtension.Translate("Okay"));
               
        }
    }
}