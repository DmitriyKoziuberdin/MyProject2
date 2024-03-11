using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;
using Restaurant.Interfaces;

namespace Restaurant.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var isExist = await _orderRepository.AnyOrderById(id);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var orderId = await _orderRepository.GetOrderById(id);
            var orderResponse = new OrderDto
            {
                OrderName = orderId.OrderName,
                Price = orderId.Price
            };
            return orderResponse;
        }

        public async Task CreateOrder(OrderCreationDto order)
        {
            await _orderRepository.CreateOrder(new Order
            {
                OrderName = order.OrderName,
                Price = order.Price
            });
        }

        public async Task<OrderDto> UpdateOrder(int orderId, OrderCreationDto order)
        {
            var isExist = await _orderRepository.AnyOrderById(orderId);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var newOrder = new Order
            {
                Id = orderId,
                OrderName = order.OrderName,
                Price = order.Price,
            };

            await _orderRepository.UpdateOrder(newOrder);
            Order orderResponse = await _orderRepository.GetOrderById(newOrder.Id);
            return new OrderDto
            {
                OrderName = orderResponse.OrderName,
                Price = orderResponse.Price
            };
        }

        public async Task DeleteOrder(int id)
        {
            var isExist = await _orderRepository.AnyOrderById(id);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            await _orderRepository.DeleteOrder(id);
        }


        public async Task<OrderCountDto> GetOrderSummary(string orderName)
        {
            var isExist = await _orderRepository.AnyOrderByName(orderName);
            if (!isExist)
            {
                throw new InvalidOperationException();
            }

            var clientsWithOrderName = await _orderRepository.GetClientCountByOrderName(orderName);

            var orderSummary = new OrderCountDto
            {
                TotalClientCount = clientsWithOrderName.Count,
                ClientEmails = clientsWithOrderName.Select(client => client.Email).ToList()
            };

            return orderSummary;
        }
    }
}
