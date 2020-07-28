using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingTicketCore.Models
{
    public class TicketModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime DateBuy { get; set; }
        [ForeignKey("SessionId "), JsonIgnore]
        public virtual SessionModel Session { get; set; }
        [Required]
        public string SessionId { get; set; }

        public int CountTickets { get; set; }
        public double Price { get; set; }
    }

    public class TicketModelView
    {
        public string SessionId { get; set; }

        public int CountTickets { get; set; }
    }
}
