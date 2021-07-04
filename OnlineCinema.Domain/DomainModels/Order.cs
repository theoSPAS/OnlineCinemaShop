using OnlineCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public OnlineCinemaTicketUser User { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
