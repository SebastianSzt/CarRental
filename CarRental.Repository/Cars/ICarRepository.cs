using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Repository.Cars
{
    public interface ICarRepository
    {
        Task<Car?> GetCarByIdAsync(int id);
        Task<List<Car>> GetAllCarsAsync();
        Task<bool> SaveCarAsync(Car car);
        Task<bool> DeleteCarAsync(int id);
    }
}
