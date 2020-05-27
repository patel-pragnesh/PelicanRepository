using System;
using System.IO;
using DellyShopApp.iOS.Persistance;
using DellyShopApp.Persistance;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLLiteDb))]
namespace DellyShopApp.iOS.Persistance
{
    public class SQLLiteDb : ISQLLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentPath, "PelicanSQlite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
