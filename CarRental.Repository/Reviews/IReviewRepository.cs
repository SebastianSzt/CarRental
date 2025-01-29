using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Repository.Reviews
{
    public interface IReviewRepository
    {
        Task<Review?> GetReviewByIdAsync(int id);
        Task<List<Review>> GetAllReviewsAsync();
        Task<List<Review>> GetReviewsByCarIdAsync(int carId);
        Task<bool> SaveReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int id);
    }
}
