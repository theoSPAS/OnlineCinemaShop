using OnlineCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Services.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(List<MailMessage> mails);

    }
}
