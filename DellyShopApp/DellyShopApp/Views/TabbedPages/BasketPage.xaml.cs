using Acr.UserDialogs;
using DellyShopApp.DAL.DAO;
using DellyShopApp.Models;
using DellyShopApp.ViewModel;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.ModalPages;
using DellyShopApp.Views.Pages;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage
    {

        private readonly BasketPageVm _basketVm = new BasketPageVm();

        public BasketPage()
        {
            InitializeComponent();
            BindingContext = new BasketViewModel();

        }
        BasketViewModel ViewModel
        {
            get
            {
                return (BasketViewModel)(BindingContext);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadData();
            //TotalPrice.Text = $"RS:{BaseTotalPrice + 12}";
            _basketVm.ProcutListModel = ProcutListModel;
            //BasketItems.ItemsSource = _basketVm.ProcutListModel;
        }


        /// <summary>
        /// Go to Address Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAddressClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddNewAddressPage());
        }

        private async void ContinueClick(object sender, EventArgs e)
        {
            var currentOrder = ViewModel.GetOrderList();
            await Navigation.PushAsync(new SelectCreditCardPage(currentOrder), true);
        }
        /// <summary>
        /// Delete Visible Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemSwipe(object sender, SwipedEventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (e.Parameter != null)
            {
                var prod = (Product)e.Parameter;
                var basketItem = new BasketItem
                {
                    Id = prod.Id,
                    ProductId = prod.ProductId
                };
                ViewModel.DeleteBasketItem(basketItem);
            }


        }
        /// <summary>
        /// Delete Visible Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndeleteItem(object sender, SwipedEventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is ProductListModel item)
            {
                item.VisibleItemDelete = false;
                VisibleDelete(item.Id);
            }
        }

        private void VisibleDelete(int id)
        {
            var items = _basketVm.ProcutListModel.Where(x => x.Id != id);
            foreach (var item in items)
            {
                item.VisibleItemDelete = false;
            }
        }

        private async void ClickItem(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            //await Navigation.PushAsync(new ProductDetail(item));
        }
    }
}