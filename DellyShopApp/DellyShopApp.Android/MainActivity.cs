﻿using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using CarouselView.FormsPlugin.Android;
using DellyShopApp.Droid.Renderers;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;
namespace DellyShopApp.Droid
{
    [Activity(Label = "DellyShopApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            initFontScale();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            ///For Performance 
            Forms.SetFlags("FastRenderers_Experimental");

            AndroidEnvironment.UnhandledExceptionRaiser -= StoreLogger;
            AndroidEnvironment.UnhandledExceptionRaiser += StoreLogger;

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CardsViewRenderer.Preserve();
            CarouselViewRenderer.Init();
            CachedImageRenderer.InitImageViewHandler();

            LoadApplication(new App());
        }
        /// <summary>
        /// Global try catch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoreLogger(object sender, RaiseThrowableEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }
        /// <summary>
        /// All app Font size 1
        /// </summary>
        private void initFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1;
            //0.85 small, 1 standard, 1.15 big，1.3 more bigger ，1.45 supper big
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.Resources.UpdateConfiguration(configuration, metrics);
        }
    }
}