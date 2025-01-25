using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repository.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var user = await DbContext.Users
                .Include(x => x.Rentals)
                .Include(x => x.Reviews)
                .SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await DbContext.Users
                .Include(x => x.Rentals)
                .Include(x => x.Reviews)
                .ToListAsync();

            return users;
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            if (user == null)
                return false;

            DbContext.Entry(user).State = user.Id == default(string) ? EntityState.Added : EntityState.Modified;
            
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

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
                return true;

            DbContext.Users.Remove(user);

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
