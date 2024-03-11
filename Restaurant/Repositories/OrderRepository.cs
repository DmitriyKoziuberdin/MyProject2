using Microsoft.EntityFrameworkCore;
using Restaurant.AppDb;
using Restaurant.Entity;
using Restaurant.Interfaces;

namespace Restaurant.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var orderById = await _context.Orders.FirstOrDefaultAsync(orderId => orderId.Id == id);
            return orderById;
        }

        public async Task CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
           _context.Orders.Update(order); 
           await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteOrder(int id)
        {
            var deletingOrder = await _context.Orders
                .Where(orderId=>orderId.Id == id)
                .ExecuteDeleteAsync();
            return deletingOrder;
        }

        public async Task<bool> AnyOrderById(int orderId)
        {
            return await _context.Orders.AnyAsync(id => id.Id == orderId);
        }

        public async Task<bool> AnyOrderByName(string name)
        {
            return await _context.Orders.AnyAsync(orderName => orderName.OrderName == name);
        }

        public async Task<List<Client>> GetClientCountByOrderName(string orderName)
        {
            return await _context.Orders
               .Where(o => o.OrderName == orderName)
               .SelectMany(o => o.OrderHistories.Select(oh => oh.Client))
               .Distinct()
               .ToListAsync();
        }
    }
}
