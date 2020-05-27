using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DellyShopApp.Models;

namespace DellyShopApp.DAL.DAO
{
    public interface IBasketDAO
    {
        Task<bool> AddBasketItem(BasketItem item);
        Task<List<BasketItem>> GetAllBasketItems();
    }
}
