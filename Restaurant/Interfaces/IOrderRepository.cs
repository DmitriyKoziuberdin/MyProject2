using Restaurant.Entity;

namespace Restaurant.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllOrders();
        public Task<Order> GetOrderById(int id);
        public Task CreateOrder(Order order);
        public Task UpdateOrder(Order order);
        public Task<int> DeleteOrder(int id);
        public Task<bool> AnyOrderById(int orderId);
        public Task<bool> AnyOrderByName(string name);
        public Task<List<Client>> GetClientCountByOrderName(string orderName);
    }
}
