using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository.Rentals
{
    public class RentalRepository : BaseRepository, IRentalRepository
    {
        public RentalRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            var rental = await DbContext.Rentals
                .Include(x => x.Car)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);

            return rental;
        }

        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            var rentals = await DbContext.Rentals
                .Include(x => x.Car)
                .Include(x => x.User)
                .ToListAsync();

            return rentals;
        }

        public async Task<bool> SaveRentalAsync(Rental rental)
        {
            if (rental == null)
                return false;

            DbContext.Entry(rental).State = rental.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteRentalAsync(int id)
        {
            var rental = await GetRentalByIdAsync(id);
            if (rental == null)
                return true;

            DbContext.Rentals.Remove(rental);

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
