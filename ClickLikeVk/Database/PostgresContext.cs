
using BuyingTicketCore.Helpers;
using ClickLikeVk.Models;
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
            if (LikeVkModels.ToList().Count == 0)
            {
                PutData data = new PutData();

                LikeVkModels.AddRange(data.models);

                SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<LikeVkModel> LikeVkModels { get; set; }
    }
}
