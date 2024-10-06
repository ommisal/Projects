using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using OrderManagement.DataModels.Models;
using OrderManagement.RetriveAPI.Controllers;
using OrderManagement.RetriveAPI.Interface;

namespace OrderManagement.Tests.Controllers
{
    public class OrdersControllerTests
    {
        
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            
            _mockOrderService = new Mock<IOrderService>();
            
            _controller = new OrdersController(_mockOrderService.Object);
        }

        [Fact]
        public void GetOrdersByNumber_ReturnsNotFound_WhenNoOrdersFound()
        {
            // Arrange
            string orderNumber = "NonExistentOrder";
            _mockOrderService.Setup(service => service.GetOrdersByNumberFromXml(orderNumber))
                .Returns(new List<Order>()); //Test Non-Existence of Orders

            // Act
            var result = _controller.GetOrdersByNumber(orderNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetOrdersByNumber_ReturnsOk_WhenOrdersFound()
        {
            // Arrange
            string orderNumber = "12345"; // An existing order number
            var orders = new List<Order>
            {
                new Order { OrderNumber = "12345", OrderLineNumber = "1", Name = "Product 1" },
                new Order { OrderNumber = "12345", OrderLineNumber = "2", Name = "Product 2" }
            };

            _mockOrderService.Setup(service => service.GetOrdersByNumberFromXml(orderNumber))
                .Returns(orders); // Simulate the method returning the orders

            // Act
            var result = _controller.GetOrdersByNumber(orderNumber) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var returnedOrders = Assert.IsAssignableFrom<List<Order>>(result.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public void GetOrdersByNumber_ReturnsNotFound_WhenOrderNumberDoesNotExist()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(service => service.GetOrdersByNumberFromXml("NonExistentOrder"))
                .Returns(new List<Order>()); //Test Non-Existence of Orders
            var controller = new OrdersController(mockOrderService.Object);

            // Act
            var result = controller.GetOrdersByNumber("NonExistentOrder");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetOrdersByNumber_ReturnsNotFound_WhenOrderNumberIsNull()
        {
            // Arrange
            string orderNumber = null;

            // Act
            var result = _controller.GetOrdersByNumber(orderNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        

    }


}
