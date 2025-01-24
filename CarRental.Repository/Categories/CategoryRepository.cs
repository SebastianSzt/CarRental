using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;

namespace CarRental.Repository.Categories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await DbContext.Categories
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == id);

            return category;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await DbContext.Categories
                .Include(x => x.Products)
                .ToListAsync();

            return categories;
        }

        public async Task<bool> SaveCategoryAsync(Category category)
        {
            if (category == null)
                return false;

            DbContext.Entry(category).State = category.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category == null)
                return true;

            DbContext.Categories.Remove(category);

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
