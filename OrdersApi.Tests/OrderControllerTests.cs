using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Controllers;
using OrdersApi.Models;
using OrdersApi.Services;

namespace OrdersApi.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrdersController _controller;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object);
        }

        [Fact]
        public void CreateOrder_ShouldReturn_CreatedAtAction()
        {
            // Arrange
            var newOrder = new Order { Id = 1, CustomerId = 1, Product = "Laptop", Quantity = 1, Status = "Pending" };
            _mockOrderService.Setup(service => service.CreateOrder(It.IsAny<Order>())).Returns(newOrder);

            // Act
            var result = _controller.CreateOrder(newOrder);

            // Assert
            var actionResult = result.Should().BeOfType<ActionResult<Order>>().Subject;

            // Ensure the result is of type CreatedAtActionResult
            var createdAtActionResult = actionResult.Result.Should().BeOfType<CreatedAtActionResult>().Subject;

            // Check for StatusCode and Value
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().Be(newOrder);
        }

        // Other tests for Get, Update, and Cancel actions
    }
}
