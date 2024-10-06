using Microsoft.AspNetCore.Mvc;
using OrderManagement.RetriveAPI.Interface;

namespace OrderManagement.RetriveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderNumber}")]
        public IActionResult GetOrdersByNumber(string orderNumber, string orderLineNumber = null)
        {
            var orders = _orderService.GetOrdersByNumberFromXml(orderNumber);

            if (orders == null || !orders.Any())
            {
                return NotFound(); // Return 404 if no orders found
            }

            if (!string.IsNullOrEmpty(orderLineNumber))
            {
                var specificOrder = orders.FirstOrDefault(o => o.OrderLineNumber == orderLineNumber);
                if (specificOrder == null)
                {
                    return NotFound();
                }

                return Ok(specificOrder);
            }

            return Ok(orders); // Return all orders as JSON if orderLineNumber is not provided
        }

    }
}
