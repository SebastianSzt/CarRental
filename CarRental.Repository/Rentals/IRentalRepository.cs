using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Repository.Rentals
{
    public interface IRentalRepository
    {
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<List<Rental>> GetAllRentalsAsync();
        Task<bool> SaveRentalAsync(Rental rental);
        Task<bool> DeleteRentalAsync(int id);
    }
}
