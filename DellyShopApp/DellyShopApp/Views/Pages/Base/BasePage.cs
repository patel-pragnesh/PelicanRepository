using System;
using DellyShopApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DellyShopApp.Languages;
using Xamarin.Forms;
using DellyShopApp.Helpers;

namespace DellyShopApp.Views.Pages.Base
{
    public class BasePage : ContentPage
    {
        public ObservableCollection<NotificationModel> NotificationList = new ObservableCollection<NotificationModel>();
        public ObservableCollection<ProductListModel> ProcutListModel = new ObservableCollection<ProductListModel>();
        public List<Category> CatoCategoriesList = new List<Category>();
        public List<Category> Carousel = new List<Category>();
        public List<StartList> StartList = new List<StartList>();
        public List<Category> CatoCategoriesDetail = new List<Category>();
        public List<CommentModel> CommentList = new List<CommentModel>();

        public double BaseTotalPrice = 0;
        public BasePage()
        {
            this.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            NotificationList.Add(new NotificationModel
            {
                Title = TranslateExtension.Translate("NotificatinTitle"),
                SubTitle = TranslateExtension.Translate("NotificationSubtitle"),
                Description = TranslateExtension.Translate("LoremIpsum"),
                Id = 1,
                Image = "elecronics.jpeg",
                InstertedAt = DateTime.Now

            });
            NotificationList.Add(new NotificationModel
            {

                Title = TranslateExtension.Translate("NotificatinTitle"),
                SubTitle = TranslateExtension.Translate("NotificationSubtitle"),
                Description = TranslateExtension.Translate("LoremIpsum"),
                Id = 2,
                Image = "shoes.jpg",
                InstertedAt = DateTime.Now

            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = TranslateExtension.Translate("ProcutTitle"),
                Brand = TranslateExtension.Translate("ProductBrand2"),
                Id = 1,
                Image = "fruitImageTwo",
                Price = 229.80,
                VisibleItemDelete = false,
                ProductList = new string[] { "orangeTwo", "OrangeThre" }
            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = TranslateExtension.Translate("ProcutTitle1"),
                Brand = TranslateExtension.Translate("ProductBrand1"),
                Id = 2,
                Image = "GreenBellPepperscut",
                Price = 150.50,
                VisibleItemDelete = false,
                ProductList = new string[] { "bellpeppersOne", "BelpeperTwo" }
            });
            ProcutListModel.Add(new ProductListModel
            {
                Title = TranslateExtension.Translate("ProcutTitle2"),
                Brand = TranslateExtension.Translate("ProductBrand"),
                Id = 3,
                Image = "uhtmilknew",
                Price = 213.50,
                VisibleItemDelete = false,
                ProductList = new string[] { "NestleTwo", "NestleThree" }
            });
            foreach (var item in ProcutListModel)
            {
                BaseTotalPrice += item.Price;
            }
            CatoCategoriesList.Add(new Category
            {
                CategoryName = TranslateExtension.Translate("Fruits"),
                Image = "fruits.png",
                Id = "1"
            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = TranslateExtension.Translate("Vegetables"),

                Image = "Vegi.png",
                Id = "2"
            });
            CatoCategoriesList.Add(new Category
            {
                CategoryName = TranslateExtension.Translate("Grocery"),

                Image = "glo.png",
                Id = "3"
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            StartList.Add(new StartList
            {
                StarImg = FontAwesomeIcons.Star
            });
            CommentList.Add(new CommentModel
            {
                Name = "Ufuk Sahin",
                CommentTime = "12/1/19",
                Id = 1,
                Rates = StartList
            });
            CommentList.Add(new CommentModel
            {
                Name = "Hans Goldman",
                CommentTime = "11/6/19",
                Id = 2,
                Rates = StartList.Skip(0).ToList()
            });
            CommentList.Add(new CommentModel
            {
                Name = "Jon Goodman",
                CommentTime = "12/8/19",
                Id = 3,
                Rates = StartList.Skip(1).ToList()
            });
            CatoCategoriesDetail.Add(new Category
            {
                Image = "groserryapps.jpg"
            });
            CatoCategoriesDetail.Add(new Category
            {
                Image = "spicesimage.jpg"
            });
            CatoCategoriesDetail.Add(new Category
            {
                Image = "vegitablesSlide.jpg"
            });
            Carousel.Add(new Category
            {
                Image = "FruitsCarosel.png"
            });
            Carousel.Add(new Category
            {
                Image = "VegitableCarosel.png"
            });
            Carousel.Add(new Category
            {
                Image = "GrosseryCarosel.png"
            });
        }
    }
}