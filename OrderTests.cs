using OrderManagement.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests.Models
{
    public class OrderTests
    {
        [Fact]
        public void Order_ShouldInitializeCorrectly_WithValidValues()
        {
            // Arrange
            var expectedOrderNumber = "17642";
            var expectedOrderLineNumber = "0001";
            var expectedProductNumber = "2000-19";
            var expectedQuantity = 0;
            var expectedName = "Small house";
            var expectedDescription = "A normal small house";
            var expectedPrice = 5.99; 
            var expectedProductGroup = "Normal";
            var expectedOrderDate = new DateTime(2013, 5, 10);
            var expectedCustomerName = "Per Andersson";
            var expectedCustomerNumber = "658254";

            // Act
            var order = new Order
            {
                OrderNumber = expectedOrderNumber,
                OrderLineNumber = expectedOrderLineNumber,
                ProductNumber = expectedProductNumber,
                Quantity = expectedQuantity,
                Name = expectedName,
                Description = expectedDescription,
                Price = expectedPrice,
                ProductGroup = expectedProductGroup,
                OrderDate = expectedOrderDate,
                CustomerName = expectedCustomerName,
                CustomerNumber = expectedCustomerNumber,
            };

            // Assert
            Assert.Equal(expectedOrderNumber, order.OrderNumber);
            Assert.Equal(expectedOrderLineNumber, order.OrderLineNumber);
            Assert.Equal(expectedProductNumber, order.ProductNumber);
            Assert.Equal(expectedQuantity, order.Quantity);
            Assert.Equal(expectedName, order.Name);
            Assert.Equal(expectedDescription, order.Description);
            Assert.Equal(expectedPrice, order.Price);
            Assert.Equal(expectedProductGroup, order.ProductGroup);
            Assert.Equal(expectedOrderDate, order.OrderDate);
            Assert.Equal(expectedCustomerName, order.CustomerName);
            Assert.Equal(expectedCustomerNumber, order.CustomerNumber);
        }

        [Fact]
        public void Order_ShouldThrowException_WhenOrderNumberIsNullOrEmpty()
        {
            // Arrange
            var order = new Order();

            // Act & Assert for null value
            var exceptionForNull = Assert.Throws<ArgumentException>(() => order.OrderNumber = null);
            Assert.Equal("Order number cannot be null or empty.", exceptionForNull.Message);

            // Act & Assert for empty string
            var exceptionForEmpty = Assert.Throws<ArgumentException>(() => order.OrderNumber = "");
            Assert.Equal("Order number cannot be null or empty.", exceptionForEmpty.Message);
        }

        /*[Fact]
        public void Order_ShouldThrowException_WhenQuantityIsNegative()
        {
            // Arrange
            var order = new Order();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => order.Quantity == -5);



            // Assert
            Assert.Equal("Quantity cannot be negative.", exception.Message);
        }

        [Fact]
        public void Order_ShouldThrowException_WhenPriceIsNegative()
        {
            // Arrange
            var order = new Order();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => order.Price = -10.99);

            // Assert
            Assert.Equal("Price cannot be negative.", exception.Message);
        }*/
    }
}
