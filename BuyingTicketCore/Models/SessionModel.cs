using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingTicketCore.Models
{
    public class SessionModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string NameFilm { get; set; }
        [Required]
        public DateTime StartFilm { get; set; }
        public string Img { get; set; }
        [Required]
        public int NumberSeats { get; set; }
        [Required]
        public string Room { get; set; }
        public virtual ICollection<TicketModel> Tickets { get; set; }
        [Required]
        public double PriceTicket { get; set; }

        public int FreeSeats
        {
            get
            {
                var seats = 0;
                if (Tickets == null)
                {
                    return NumberSeats;
                }
                foreach(var ticket in Tickets)
                {
                    seats += ticket.CountTickets;
                }
                return NumberSeats - seats;
            }
        }
    }
    public class SessionModelView
    {
        public string NameFilm { get; set; }
        public string Room { get; set; }
        public DateTime StartFilm { get; set; }
        public int NumberSeats { get; set; }
        public double PriceTicket { get; set; }
        public string Img { get; set; }
    }
}
