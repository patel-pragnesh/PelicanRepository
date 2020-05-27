using System;
using System.Threading.Tasks;
using DellyShopApp.DAL.Entity;
using DellyShopApp.Persistance;
using SQLite;

namespace DellyShopApp.DAL
{
    public class DBHandler
    {
        public SQLiteAsyncConnection _connection;
        public DBHandler(ISQLLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<ENTProduct>().Wait();
        }

        #region Product
        public async Task ClearProducts()
        {
            try
            {
                await _connection.DeleteAllAsync<ENTProduct>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task SaveProduct(ENTProduct product)
        {
            try
            {
                await _connection.InsertOrReplaceAsync(product);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ENTProduct> GetProduct()
        {
            try
            {
                return await _connection.Table<ENTProduct>().FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
