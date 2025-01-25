using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Repository.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> SaveUserAsync(User user);
        Task<bool> DeleteUserAsync(string id);
    }
}
