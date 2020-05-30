using System;
using DellyShopApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage 
	{
		public RegisterPage ()
		{
            InitializeComponent ();
			BindingContext = new RegisterViewModel(this.Navigation);

        }

		RegisterViewModel ViewModel
        {
            get
            {
                return (RegisterViewModel)BindingContext;
            }
        }
		protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	     
        }
        private async void RegisteruButtonClick(object sender, EventArgs e)
	    {
            ViewModel.RegisterCustomer();

	    }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    
}
