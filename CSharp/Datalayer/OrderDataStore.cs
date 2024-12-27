using OrdersApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrdersApi.Data
{
    public class OrderDataStore : IOrderDataStore
    {
        private static List<Order> orders = new List<Order>
        {
            new Order { Id = 1, CustomerId = 1, Product = "Laptop", Quantity = 1, Status = "Pending" },
            new Order { Id = 2, CustomerId = 2, Product = "Phone", Quantity = 2, Status = "Shipped" }
        };

        public Order Add(Order order)
        {
            order.Id = orders.Max(o => o.Id) + 1;
            orders.Add(order);
            return order;
        }

        public Order GetById(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public Order Update(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.Product = order.Product;
                existingOrder.Quantity = order.Quantity;
                existingOrder.Status = order.Status;
                return existingOrder;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                orders.Remove(order);
                return true;
            }
            return false;
        }
    }
}
