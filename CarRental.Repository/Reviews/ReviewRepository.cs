using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository.Reviews
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            var review = await DbContext.Reviews
                .Include(x => x.Car)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);

            return review;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            var reviews = await DbContext.Reviews
                .Include(x => x.Car)
                .Include(x => x.User)
                .ToListAsync();

            return reviews;
        }

        public async Task<bool> SaveReviewAsync(Review review)
        {
            if (review == null)
                return false;

            DbContext.Entry(review).State = review.Id == default(int) ? EntityState.Added : EntityState.Modified;
            
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

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review == null)
                return true;

            DbContext.Reviews.Remove(review);

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
