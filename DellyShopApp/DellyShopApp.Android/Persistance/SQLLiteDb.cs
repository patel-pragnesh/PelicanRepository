using System;
using System.IO;
using DellyShopApp.Droid.Persistance;
using DellyShopApp.Persistance;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLLiteDb))]
namespace DellyShopApp.Droid.Persistance
{
    public class SQLLiteDb : ISQLLiteDb
    {

        public SQLiteAsyncConnection GetConnection()
        {
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentPath, "PelicanSQlite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}
