using System;
using DellyShopApp.Languages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.PartialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBarPartialView
    {
        public NavigationBarPartialView(string title, bool backButtonVisible, bool isModalpage, bool favVisible)
        {
            InitializeComponent();
            Title.Text = TranslateExtension.Translate(title);
            BackButton.IsVisible = backButtonVisible;
            favImage.IsVisible = favVisible;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                if (isModalpage)
                    Navigation.PopModalAsync();
                else
                    Navigation.PopAsync();
            };

            BackButton.GestureRecognizers.Add(tapGestureRecognizer);
        }


    }
}