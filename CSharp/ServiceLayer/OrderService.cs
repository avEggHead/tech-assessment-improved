using OrdersApi.Models;
using System.Collections.Generic;
using OrdersApi.Data;

namespace OrdersApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDataStore _orderDataStore;

        public OrderService(IOrderDataStore orderRepository)
        {
            _orderDataStore = orderRepository;
        }

        public Order CreateOrder(Order newOrder)
        {
            return _orderDataStore.Add(newOrder);
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return _orderDataStore.GetOrdersByCustomer(customerId);
        }

        public Order UpdateOrder(int id, Order updatedOrder)
        {
            var order = _orderDataStore.GetById(id);
            if (order != null)
            {
                order.Product = updatedOrder.Product;
                order.Quantity = updatedOrder.Quantity;
                order.Status = updatedOrder.Status;
                return _orderDataStore.Update(order);
            }
            return null;
        }

        public bool CancelOrder(int id)
        {
            var order = _orderDataStore.GetById(id);
            if (order != null) {
                if(order.Status != "Pending")
                {
                    return false;
                }
            }


            return _orderDataStore.Delete(id);
        }

        public Order GetOrder(int id)
        {
            return _orderDataStore.GetById(id);
        }
    }
}
