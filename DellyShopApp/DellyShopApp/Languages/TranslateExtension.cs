using Plugin.Multilingual;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DellyShopApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Languages
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "DellyShopApp.Languages.AppResources";
        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));
        public string Text { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                string translation ;
                if (Text == null)
                    return "";
                var ci = CrossMultilingual.Current.CurrentCultureInfo;
                if (Settings.SelectLanguage == "")
                {
                   
                    translation = ci.Name.Contains("ar")  ?  ResMgr.Value.GetString(Text, new CultureInfo("ar", false)) : ResMgr.Value.GetString(Text, new CultureInfo("en", false));
                }
                else
                {
                    translation = ResMgr.Value.GetString(Text, new CultureInfo(Settings.SelectLanguage));
                }
               
                if (translation == null)
                {
#if DEBUG
                    throw new ArgumentException(
                        String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                        "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
                }
                return translation;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }

        }

        public static string Translate(string text)
        {
            if (text == null)
                return "";
            string translate ;
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            if (Settings.SelectLanguage == "")
            {
                translate = ci.Name.Contains("ar") ?  ResMgr.Value.GetString(text, new CultureInfo("ar", false)) : ResMgr.Value.GetString(text, new CultureInfo("en", false));
            }
            else
            {
                translate = ResMgr.Value.GetString(text, new CultureInfo(Settings.SelectLanguage));
            }
            return translate;
        }
    }
}
