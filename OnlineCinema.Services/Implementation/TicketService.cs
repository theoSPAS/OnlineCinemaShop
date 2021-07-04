using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Domain.DTO;
using OnlineCinema.Repository.Interface;
using OnlineCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCinema.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddTicketToCartDTO item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var shoppingCart = user.UserCart;

            if(item.TicketId != null && shoppingCart != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if(ticket != null)
                {
                    TicketInShoppingCart toAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = shoppingCart,
                        ShoppingCartId = shoppingCart.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInShoppingCartRepository.Insert(toAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddTicketToCartDTO GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddTicketToCartDTO dto = new AddTicketToCartDTO
            {
                ChoosenTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return dto;
        }

        public void UpdateExistingTicket(Ticket t)
        {
            this._ticketRepository.Update(t);
        }
    }
}
