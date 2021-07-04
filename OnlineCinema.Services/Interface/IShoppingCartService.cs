using OnlineCinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO GetShoppingCartInfo(string userId);
        bool DeleteTicketFromShoppingCart(string userId, Guid id);
        bool OrderNow(string userId);
    }
}
