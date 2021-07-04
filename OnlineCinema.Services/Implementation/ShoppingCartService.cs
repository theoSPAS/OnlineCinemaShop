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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<MailMessage> _mailRepository;

        public ShoppingCartService(IRepository<MailMessage> mailRepository,IUserRepository userRepository,IRepository<TicketInOrder> ticketInOrderRepository,IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository) 
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _userRepository = userRepository;
            _mailRepository = mailRepository;
        }

        public bool DeleteTicketFromShoppingCart(string userId, Guid id)
        {
            if(!string.IsNullOrEmpty(userId) && id!=null)
            {
                var user = this._userRepository.Get(userId);

                var shoppingCart = user.UserCart;

                var deleteItem = shoppingCart.TicketInShoppingCarts
                    .Where(z => z.TicketId.Equals(id))
                    .FirstOrDefault();

                shoppingCart.TicketInShoppingCarts.Remove(deleteItem);

                this._shoppingCartRepository.Update(shoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO GetShoppingCartInfo(string userId)
        {
            var user = this._userRepository.Get(userId);

            var shoppingCart = user.UserCart;

            var tickets = shoppingCart.TicketInShoppingCarts.ToList();

            var ticketsPrice = tickets.Select(z => new
            {
                TicketPrice = z.Ticket.TicketPrice,
                Quantity = z.Quantity
            }).ToList();

            var price = 0;

            foreach(var t in ticketsPrice)
            {
                price += t.Quantity * t.TicketPrice;
            }

            ShoppingCartDTO dto = new ShoppingCartDTO
            {
                TicketInShoppingCarts = tickets,
                TotalPrice = price
            };

            return dto;
        }

        public bool OrderNow(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var user = this._userRepository.Get(userId);

                var shoppingCart = user.UserCart;


                MailMessage message = new MailMessage();
                message.To = user.Email;
                message.Subject = "Created Order";
                message.Status = false;


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var list = shoppingCart.TicketInShoppingCarts.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    ChosenTicket = z.Ticket,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();


                ticketInOrders.AddRange(list);

                StringBuilder builder = new StringBuilder();

                var price = 0;

                builder.AppendLine("Order detailrs: ");
                for(int i=1; i<= list.Count(); i++)
                {
                    var ticket = list[i - 1];

                    price += ticket.Quantity * ticket.ChosenTicket.TicketPrice;

                    builder.AppendLine(i.ToString() + "with price of " + ticket.ChosenTicket.TicketPrice + " and quantity " + ticket.Quantity);
                }

                builder.AppendLine("Price to pay " + price.ToString());

                message.Content = builder.ToString();

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                user.UserCart.TicketInShoppingCarts.Clear();

                this._userRepository.Update(user);

                this._mailRepository.Insert(message);

                return true;
            }
            return false;
        }
    }
}
