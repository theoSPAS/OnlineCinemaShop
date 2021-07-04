using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
