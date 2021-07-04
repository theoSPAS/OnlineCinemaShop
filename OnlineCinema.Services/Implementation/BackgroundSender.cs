using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Repository.Interface;
using OnlineCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Services.Implementation
{
    public class BackgroundSender : IBackgroundSender
    {
        private readonly IMailService _mailService;
        private readonly IRepository<MailMessage> _mailRepository;


        public BackgroundSender(IMailService emailService, IRepository<MailMessage> mailRepository)
        {
            _mailService = emailService;
            _mailRepository = mailRepository;
        }

        public async Task DoWork()
        {
            await _mailService.SendEmailAsync(_mailRepository.GetAll().Where(z => !z.Status).ToList());
        }
    }
}
