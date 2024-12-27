using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OrdersApi.Services;
using OrdersApi.Models;
using Serilog;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {
            Log.Information("Creating a new order with product: {Product}", newOrder.Product);

            var createdOrder = _orderService.CreateOrder(newOrder);

            if (createdOrder == null)
            {
                Log.Error("Failed to create order for product: {Product}", newOrder.Product);
                return BadRequest();
            }

            Log.Information("Successfully created order with ID: {OrderId}", createdOrder.Id);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
        {
            Log.Information("Fetching orders for customer ID: {CustomerId}", customerId);

            var orders = _orderService.GetOrdersByCustomer(customerId);
            if (orders == null || orders.Count == 0)
            {
                Log.Warning("No orders found for customer ID: {CustomerId}", customerId);
                return NotFound(new { message = "No orders found for this customer." });
            }

            Log.Information("Successfully retrieved {OrderCount} orders for customer ID: {CustomerId}", orders.Count, customerId);
            return Ok(orders);
        }

        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            Log.Information("Updating order with ID: {OrderId}", id);

            var order = _orderService.UpdateOrder(id, updatedOrder);
            if (order == null)
            {
                Log.Warning("Order with ID: {OrderId} not found for update", id);
                return NotFound(new { message = "Order not found." });
            }

            Log.Information("Successfully updated order with ID: {OrderId}", order.Id);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public ActionResult CancelOrder(int id)
        {
            Log.Information("Canceling order with ID: {OrderId}", id);

            var result = _orderService.CancelOrder(id);
            if (!result)
            {
                Log.Warning("Order with ID: {OrderId} not found for cancellation", id);
                return NotFound(new { message = "Order not found." });
            }

            Log.Information("Successfully canceled order with ID: {OrderId}", id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            Log.Information("Fetching order with ID: {OrderId}", id);

            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                Log.Warning("Order with ID: {OrderId} not found", id);
                return NotFound(new { message = "Order not found." });
            }

            Log.Information("Successfully retrieved order with ID: {OrderId}", order.Id);
            return Ok(order);
        }
    }
}
