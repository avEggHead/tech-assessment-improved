using OrdersApi.Models;
using System.Collections.Generic;

namespace OrdersApi.Data
{
    public interface IOrderDataStore
    {
        Order Add(Order order);
        Order GetById(int id);
        List<Order> GetOrdersByCustomer(int customerId);
        Order Update(Order order);
        bool Delete(int id);
    }
}
