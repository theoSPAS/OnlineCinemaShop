using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
