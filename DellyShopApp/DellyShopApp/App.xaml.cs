using DellyShopApp.Helpers;
using DellyShopApp.Views.CustomView;
using Plugin.SharedTransitions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DellyShopApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            MainPage = new SharedTransitionNavigationPage(new MainPage());
            App.Current.MainPage.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
    }
}
