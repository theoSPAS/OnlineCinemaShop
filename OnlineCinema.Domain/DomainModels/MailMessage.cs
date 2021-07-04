using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCinema.Domain.DomainModels
{
    public class MailMessage : BaseEntity
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Boolean Status { get; set; }
    }
}
