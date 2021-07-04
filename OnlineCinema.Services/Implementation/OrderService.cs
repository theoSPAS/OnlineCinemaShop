using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Repository.Interface;
using OnlineCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }

        public List<Order> GetOrders()
        {
            return this._orderRepository.GetOrders();
        }
    }
}
