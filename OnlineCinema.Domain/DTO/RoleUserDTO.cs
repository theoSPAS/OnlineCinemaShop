using OnlineCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Domain.DTO
{
    public class RoleUserDTO
    {
        public List<string> Roles { get; set; }

        public List<OnlineCinemaTicketUser> Users { get; set; }
    }
}
