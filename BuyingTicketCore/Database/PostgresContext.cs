using BuyingTicketCore.Helpers;
using BuyingTicketCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingTicketCore.Database
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            if (Sessions.ToList().Count == 0)
            {
                PutData data = new PutData();
                Sessions.AddRange(data.sessionModels);
                Tickets.AddRange(data.ticketModels);

                SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<SessionModel> Sessions { get; set; }
        public virtual DbSet<TicketModel> Tickets { get; set; }
    }
}
