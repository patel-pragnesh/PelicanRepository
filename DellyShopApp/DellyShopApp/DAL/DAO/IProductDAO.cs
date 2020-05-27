using System;
using System.Threading.Tasks;
using DellyShopApp.Models;

namespace DellyShopApp.DAL.DAO
{
    interface IProductDAO
    {
        Task<Product> GetProduct();
        Task<Product> UpdateProduct(Product product);
    }
}
