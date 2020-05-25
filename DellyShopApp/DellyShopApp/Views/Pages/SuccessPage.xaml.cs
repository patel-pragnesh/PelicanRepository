using System;
using System.Collections.Generic;
using DellyShopApp.Models;
using Plugin.SharedTransitions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPage
    {
        private List<ProductListModel> _procutListModel = new List<ProductListModel>();

        public SuccessPage()
        {
            _procutListModel.Add(new ProductListModel
            {
                Title = "Fresh Orange",
                Brand = "Fruits(Local)",
                Id = 1,
                Image = "fruitImageTwo",
                Price = 229.80,
                VisibleItemDelete = false
            });
            _procutListModel.Add(new ProductListModel
            {
                Title = "Green Bell Papper",
                Brand = "Vegitable",
                Id = 3,
                Image = "GreenBellPepperscut",
                Price = 150.50,
                VisibleItemDelete = false
            });
            _procutListModel.Add(new ProductListModel
            {
                Title = "Nestle Fresh Milk",
                Brand = "Grocery",
                Id = 2,
                Image = "uhtmilknew",
                Price = 213.50,
                VisibleItemDelete = false
            });
            InitializeComponent();
            BasketItems.ItemsSource = _procutListModel;
        }

        private async void ContinueClick(object sender, EventArgs e)
        {
            //.Current.MainPage = new HomeTabbedPage();
            Application.Current.MainPage = new SharedTransitionNavigationPage(new HomeTabbedPage());
        }
    }
}