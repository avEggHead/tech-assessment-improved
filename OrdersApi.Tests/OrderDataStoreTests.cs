using Xunit;
using FluentAssertions;
using OrdersApi.Models;
using OrdersApi.Data;
using System.Linq;

namespace OrdersApi.Tests
{
    public class OrderDataStoreTests
    {
        private readonly IOrderDataStore _dataStore;

        public OrderDataStoreTests()
        {
            _dataStore = new OrderDataStore();
        }

        [Fact]
        public void Add_ShouldReturn_OrderWithId()
        {
            // Arrange
            var newOrder = new Order { CustomerId = 1, Product = "Laptop", Quantity = 1, Status = "Pending" };

            // Act
            var result = _dataStore.Add(newOrder);

            // Assert
            result.Id.Should().BeGreaterThan(0);  // Ensure the ID is assigned
            result.CustomerId.Should().Be(newOrder.CustomerId);
            result.Product.Should().Be(newOrder.Product);
        }

        [Fact]
        public void GetOrdersByCustomer_ShouldReturn_Orders()
        {
            // Arrange
            var customerId = 1;
            _dataStore.Add(new Order { CustomerId = customerId, Product = "Laptop", Quantity = 1, Status = "Pending" });

            // Act
            var result = _dataStore.GetOrdersByCustomer(customerId);

            // Assert
            result.Should().NotBeEmpty();
            result.All(o => o.CustomerId == customerId).Should().BeTrue();
        }

        // Other tests for GetById, Update, Delete, etc.
    }
}
