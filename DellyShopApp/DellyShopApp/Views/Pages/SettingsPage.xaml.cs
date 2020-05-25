using System;
using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using Plugin.SharedTransitions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DellyShopApp.ThemeManager;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        string[] languages = { "English", "العربية" };
        public SettingsPage()
        {

            InitializeComponent();
            languageLabel.Text = Settings.SelectLanguage == "ar" ? "العربية" : "English";
        }

        protected void Toggled(object sender, ToggledEventArgs args)
        {
            if (args.Value)
            {
                ThemeManager.ChangeTheme(0);
            }
            else
            {
                ThemeManager.ChangeTheme(Themes.White);
            }
        }
        protected void LogOutClick(object sender, EventArgs args)
        {
            Application.Current.MainPage = new SharedTransitionNavigationPage(new MainPage());
        }
        protected async void SelectLanguage(object sender, EventArgs args)
        {
            var selectlanguage = await DisplayActionSheet(TranslateExtension.Translate("SelectLanguage"), TranslateExtension.Translate("Cancel"), TranslateExtension.Translate("Cancel"), languages);
            switch (selectlanguage)
            {
                case "English":
                    Settings.SelectLanguage = "en";
                    Application.Current.MainPage = new SharedTransitionNavigationPage(new MainPage());
                    break;
                case "العربية":
                    Settings.SelectLanguage = "ar";
                    Application.Current.MainPage = new SharedTransitionNavigationPage(new MainPage());
                    break;
                default:
                    break;
            }
        }
    }
}