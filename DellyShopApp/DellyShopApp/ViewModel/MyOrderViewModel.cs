using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using DellyShopApp.Models;
using DellyShopApp.Network;
using Xamarin.Forms;

namespace DellyShopApp.ViewModel
{
    public class MyOrderViewModel : BaseVm
    {
        protected readonly INavigation Navigation;
        private ObservableCollection<OrderByCustomerItem> _orderItems;
        public ObservableCollection<OrderByCustomerItem> OrderItems
        {
            get
            {
                return _orderItems;
            }
            set
            {
                SetProperty(ref _orderItems, value);
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        private ObservableCollection<OrdersByCustomer> _orders;
        public ObservableCollection<OrdersByCustomer> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                SetProperty(ref _orders, value);
                OnPropertyChanged(nameof(Orders));
            }
        }
        public MyOrderViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Loading My Orders, Please wait..."))
                {
                    var orders = await DsApi.GetInstance().GetOrdersByCustomer(AuthenticatedUser.LoggedInUser.CustomerId);
                    SetData(orders);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch(Exception)
            {
                UserDialogs.Instance.HideLoading();
            }


        }

        private void SetData(List<OrdersByCustomer> orders)
        {
            var data = new ObservableCollection<OrdersByCustomer>();
            var itemData = new ObservableCollection<OrderByCustomerItem>();

            foreach (OrdersByCustomer resp in orders)
            {
                var domitems = new List<OrderByCustomerItem>();
                if (resp.Items != null && resp.Items.Count > 0)
                {
                    foreach (OrderByCustomerItem item in resp.Items)
                    {
                        domitems.Add(new OrderByCustomerItem()
                        {
                            DiscountAmount = item.DiscountAmount,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Qty = item.Qty,
                            TotalAmount = item.TotalAmount
                        });

                        //temp
                        itemData.Add(new OrderByCustomerItem()
                        {
                            DiscountAmount = item.DiscountAmount,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Qty = item.Qty,
                            TotalAmount = item.TotalAmount,
                            OrderedDate = resp.OrderedDate,
                            OrderNumber = resp.OrderNumber,
                            OrderStatus = resp.OrderStatus 
                        });
                    }
                }
                data.Add(new OrdersByCustomer()
                {
                    SalesOrderId = resp.SalesOrderId,
                    PicCustomer = resp.PicCustomer,
                    Branch = resp.Branch,
                    DeliveryAddress = resp.DeliveryAddress,
                    DeliveryDate = resp.DeliveryDate,
                    OrderedDate = resp.OrderedDate,
                    OrderNumber = resp.OrderNumber,
                    OrderStatus = resp.OrderStatus,
                    ReferenceNumberExternal = resp.ReferenceNumberExternal,
                    TotalDiscountAmount = resp.TotalDiscountAmount,
                    TotalOrderAmount = resp.TotalOrderAmount,
                    Items = domitems

                });
            }

            Orders = data;
            OrderItems = itemData;
        }
    }
}
