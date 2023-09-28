using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task<Order> GetOrder()
        {
            var maxPrice = await _db.Orders.MaxAsync(o => o.Price * o.Quantity);

            return await _db.Orders.FirstAsync(o => o.Price * o.Quantity == maxPrice);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _db.Orders.Where(o => o.Quantity > 10).ToListAsync();
        }
    }
}
