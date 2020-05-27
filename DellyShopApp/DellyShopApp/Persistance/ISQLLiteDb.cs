using System;
using SQLite;

namespace DellyShopApp.Persistance
{
    public interface ISQLLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
