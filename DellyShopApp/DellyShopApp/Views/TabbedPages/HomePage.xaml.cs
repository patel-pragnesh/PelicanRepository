using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.ViewModel;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        public HomePage()
        {
            this.BindingContext = new HomeViewModel();
            ProcutListModel.Insert(0, new ProductListModel
            {
                Title = TranslateExtension.Translate("ProcutTitle3"),
                Brand = TranslateExtension.Translate("ProductBrand3"),

                Id = 4,
                Image = "fruitApple",
                Price = 229.80,
                VisibleItemDelete = false,
                ProductList = new string[] { "appleOne", "appleTwo" }
            });

            InitializeComponent();
            CategoryList.ItemsSource = CatoCategoriesList;
            CarouselView.ItemsSource = Carousel;
            BestSellerList.ItemsSource = ProcutListModel;
            //PreviousViewedList.ItemsSource = ProcutListModel;
            MostNews.FlowItemsSource = ProcutListModel;
        }

        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }

        private async void ClickCategory(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stack)) return;
            if (!(stack.BindingContext is Category ca)) return;
            await Navigation.PushAsync(new CategoryDetailPage(ca));
        }
    }
}