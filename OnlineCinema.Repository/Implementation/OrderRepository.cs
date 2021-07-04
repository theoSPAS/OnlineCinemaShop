using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.TicketInOrders)
                .Include("TicketInOrders.ChosenTicket")
                .SingleOrDefaultAsync(z => z.Id.Equals(model.Id))
                .Result;
        }

        public List<Order> GetOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.TicketInOrders)
                .Include("TicketInOrders.ChosenTicket")
                .ToListAsync()
                .Result;
        }
    }
}
