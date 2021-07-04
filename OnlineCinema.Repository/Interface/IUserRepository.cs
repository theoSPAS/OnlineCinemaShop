using OnlineCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<OnlineCinemaTicketUser> GetAll();
        OnlineCinemaTicketUser Get(string id);
        void Insert(OnlineCinemaTicketUser entity);
        void Update(OnlineCinemaTicketUser entity);
        void Delete(OnlineCinemaTicketUser entity);
    }
}
