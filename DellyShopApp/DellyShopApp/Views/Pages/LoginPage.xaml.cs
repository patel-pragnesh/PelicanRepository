using System;
using DellyShopApp.Languages;
using DellyShopApp.ViewModel;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        LoginViewModel ViewModel
        {
            get
            {
                return (LoginViewModel)(BindingContext);
            }
        }

        private async void LoginButtonClick(object sender, EventArgs e)
        {
            ViewModel.CustomerLogin();
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void ForgetPassClick(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync(TranslateExtension.Translate("ForgotPass"),
                TranslateExtension.Translate("EnterEmailAddress"));
            if (string.IsNullOrEmpty(result)) return;
            await DisplayAlert(TranslateExtension.Translate("Success"),
                TranslateExtension.Translate("SuccessSendEmail")
                + " " + result, TranslateExtension.Translate("Okay"));
        }
    }
}