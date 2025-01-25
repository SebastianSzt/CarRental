using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository.Cars
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            var car = await DbContext.Cars
                .Include(x => x.Rentals)
                .Include(x => x.Reviews)
                .SingleOrDefaultAsync(x => x.Id == id);

            return car;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await DbContext.Cars
                .Include(x => x.Rentals)
                .Include(x => x.Reviews)
                .ToListAsync();

            return cars;
        }

        public async Task<bool> SaveCarAsync(Car car)
        {
            if (car == null)
                return false;

            DbContext.Entry(car).State = car.Id == default(int) ? EntityState.Added : EntityState.Modified;
            
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

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await GetCarByIdAsync(id);
            if (car == null)
                return true;

            DbContext.Cars.Remove(car);

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
