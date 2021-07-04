using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain.Identity;
using OnlineCinema.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCinema.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<OnlineCinemaTicketUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<OnlineCinemaTicketUser>();
        }
        public IEnumerable<OnlineCinemaTicketUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public OnlineCinemaTicketUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.TicketInShoppingCarts")
               .Include("UserCart.TicketInShoppingCarts.Ticket")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(OnlineCinemaTicketUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(OnlineCinemaTicketUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(OnlineCinemaTicketUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
