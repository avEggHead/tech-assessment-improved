using OrdersApi.Models;
using System.Collections.Generic;

namespace OrdersApi.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order newOrder);
        List<Order> GetOrdersByCustomer(int customerId);
        Order UpdateOrder(int id, Order updatedOrder);
        bool CancelOrder(int id);
        Order GetOrder(int id);
    }
}
