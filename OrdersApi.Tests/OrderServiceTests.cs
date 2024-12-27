using Moq;
using Xunit;
using FluentAssertions;
using OrdersApi.Models;
using OrdersApi.Data;
using OrdersApi.Services;

namespace OrdersApi.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderDataStore> _mockOrderDataStore;
        private readonly IOrderService _service;

        public OrderServiceTests()
        {
            _mockOrderDataStore = new Mock<IOrderDataStore>();
            _service = new OrderService(_mockOrderDataStore.Object);
        }

        [Fact]
        public void CreateOrder_ShouldReturn_CreatedOrder()
        {
            // Arrange
            var newOrder = new Order { CustomerId = 1, Product = "Laptop", Quantity = 1, Status = "Pending" };
            _mockOrderDataStore.Setup(store => store.Add(It.IsAny<Order>())).Returns(newOrder);

            // Act
            var result = _service.CreateOrder(newOrder);

            // Assert
            result.Should().BeEquivalentTo(newOrder);
        }

        // Other tests for UpdateOrder, CancelOrder, etc.
        [Fact]
        public void CancelOrder_ShouldNotCancel_WhenStatusIsNotPending()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                Product = "Laptop",
                Quantity = 1,
                Status = "Shipped"  // Status is not "Pending"
            };

            _mockOrderDataStore.Setup(store => store.GetById(order.Id)).Returns(order);

            // Act
            var result = _service.CancelOrder(order.Id);

            // Assert
            result.Should().BeFalse();  // We expect false because the status is not "Pending"
            _mockOrderDataStore.Verify(store => store.Delete(It.IsAny<int>()), Times.Never);  // Verify that Delete was not called
        }
    }
}
