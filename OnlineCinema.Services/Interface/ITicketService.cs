using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        AddTicketToCartDTO GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddTicketToCartDTO item, string userID);
    }
}
