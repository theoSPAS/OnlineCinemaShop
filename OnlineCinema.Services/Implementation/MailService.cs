using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using OnlineCinema.Domain;
using OnlineCinema.Domain.DomainModels;
using OnlineCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Services.Implementation
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(List<OnlineCinema.Domain.DomainModels.MailMessage> mails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach (var m in mails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_mailSettings.SendersName, _mailSettings.SmtpUserName),
                    Subject = m.Subject
                };
                emailMessage.From.Add(new MailboxAddress(_mailSettings.EmailDisplayName, _mailSettings.SmtpUserName));

                emailMessage.Body = new TextPart(TextFormat.Plain) { Text = m.Content };

                emailMessage.To.Add(new MailboxAddress(m.To));

                messages.Add(emailMessage);
            }



            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _mailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpServerPort, socketOption);

                    if (!string.IsNullOrEmpty(_mailSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_mailSettings.SmtpUserName, _mailSettings.SmtpPassword);
                    }

                    foreach (var m in messages)
                    {
                        await smtp.SendAsync(m);
                    }

                    await smtp.DisconnectAsync(true);
                }

            }
            catch (SmtpException exception)
            {
                throw exception;
            }
        }
    }
}
