using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Domain.DTO
{
    public class AddTicketToCartDTO
    {
        public Ticket ChoosenTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
