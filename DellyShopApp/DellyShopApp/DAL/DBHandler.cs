using System;
using System.Collections.Generic;
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
            //_connection.CreateTableAsync<ENTProduct>().Wait();
            _connection.CreateTableAsync<ENTBasketItem>().Wait();
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

        public async Task SaveProducts(List<ENTProduct> products)
        {
            try
            {
                await _connection.InsertAllAsync(products);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ENTProduct>> GetProducts()
        {
            try
            {
                return await _connection.Table<ENTProduct>().ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Basket
        public async Task ClearBasket()
        {
            try
            {
                await _connection.DeleteAllAsync<ENTBasketItem>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddProductToBasket(ENTBasketItem basketItem)
        {
            try
            {
                await _connection.InsertAsync(basketItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ENTBasketItem>> GetAllBasketItems()
        {
            try
            {
                return await _connection.Table<ENTBasketItem>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> ClearBasketItem(ENTBasketItem item)
        {
            try
            {
                return await _connection.DeleteAsync<ENTBasketItem>(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
