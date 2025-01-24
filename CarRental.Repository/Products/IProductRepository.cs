using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Repository.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> SaveProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
