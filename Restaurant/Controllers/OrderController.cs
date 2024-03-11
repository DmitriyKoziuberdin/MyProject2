using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO.Incoming;
using Restaurant.DTO.Outcoming;
using Restaurant.Entity;
using Restaurant.Interfaces;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpGet("{orderId:int}")]
        public async Task<OrderDto> GetOrderById([FromRoute] int orderId)
        {
            return await _orderService.GetOrderById(orderId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto order)
        {
            await _orderService.CreateOrder(order);
            return Ok();
        }

        [HttpPut("{orderId:int}/")]
        public async Task<ActionResult<OrderDto>> UpdateOrder([FromRoute]int orderId, [FromBody] OrderCreationDto order)
        {
            return new OkObjectResult(await _orderService.UpdateOrder(orderId, order));
        }

        [HttpDelete("{orderID:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int orderID)
        {
            await _orderService.DeleteOrder(orderID);
            return Ok();
        }


        [HttpGet("summary/{orderName}")]
        public async Task<ActionResult<OrderCountDto>> GetOrderSummary(string orderName)
        {
            var orderSummary = await _orderService.GetOrderSummary(orderName);

            return Ok(orderSummary);
        }
    }
}
