using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
