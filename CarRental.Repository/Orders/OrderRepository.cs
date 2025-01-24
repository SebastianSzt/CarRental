using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model;
using CarRental.Model.Entities;

namespace CarRental.Repository.Orders
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var order = await DbContext.Orders
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == id);

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await DbContext.Orders
                .Include(x => x.Products)
                .ToListAsync();

            return orders;
        }

        public async Task<bool> SaveOrderAsync(Order order)
        {
            if (order == null)
                return false;

            DbContext.Entry(order).State = order.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order == null)
                return true;

            DbContext.Orders.Remove(order);

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
