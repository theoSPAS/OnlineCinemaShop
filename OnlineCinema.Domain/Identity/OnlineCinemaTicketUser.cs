using Microsoft.AspNetCore.Identity;
using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Domain.Identity
{
    public class OnlineCinemaTicketUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

    }
}
