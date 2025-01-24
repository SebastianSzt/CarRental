using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;

namespace CarRental.Repository.Products
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await DbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Orders)
                .SingleOrDefaultAsync(x => x.Id == id);
            
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await DbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Orders)
                .ToListAsync();
            
            return products;
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            if (product == null)
                return false;

            DbContext.Entry(product).State = product.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
                return true;

            DbContext.Products.Remove(product);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
