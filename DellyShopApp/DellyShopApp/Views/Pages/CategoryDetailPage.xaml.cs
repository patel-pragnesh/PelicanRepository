using DellyShopApp.Models;
using DellyShopApp.ViewModel;
using DellyShopApp.Views.CustomView;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage
    {
        public CategoryDetailPage(ProductType category)
        {
            InitializeComponent();
            this.BindingContext = new CategoryDetailViewModel(Navigation,category);
            CarouselView.ItemsSource = CatoCategoriesDetail;
            //BestSellerList.ItemsSource = ProcutListModel;
            //PreviousViewedList.ItemsSource = ProcutListModel;
            //MostNews.FlowItemsSource = ProcutListModel;
        }
        private async void ClickCategory(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stack)) return;
            if (!(stack.BindingContext is ProductListModel ca)) return;
            //await Navigation.PushAsync(new ProductDetail(ca));
        }
        private async void BackPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void ProductDetailClick(object sender, EventArgs e)
        {
            var prod = (Product)(((TappedEventArgs)e).Parameter);
            await Navigation.PushAsync(new ProductDetail(prod));
        }
    }
}