using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;

namespace Restaurant.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllOrders();
        public Task<OrderDto> GetOrderById(int id);
        public Task CreateOrder(OrderCreationDto order);
        public Task<OrderDto> UpdateOrder(int orderId, OrderCreationDto order);
        public Task DeleteOrder(int id);

        public Task<OrderCountDto> GetOrderSummary(string orderName);

    }
}
