using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCinema.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string TicketName { get; set; }
        [Required]
        public string TicketImage { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        [Required]
        public string TicketDescription { get; set; }
        [Required]
        public DateTime TicketDate { get; set; }
        [Required]
        public int MovieRating { get; set; }
        [Required]
        public string MovieGenre { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }


    }
}
