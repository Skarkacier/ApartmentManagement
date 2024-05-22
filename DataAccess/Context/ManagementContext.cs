using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {
        }
        protected readonly IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source = DESKTOP-G69HVIR; Database = ApartmentManagement; integrated security = true; TrustServerCertificate=true;");
        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>().ToTable("Apartment");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<User>().ToTable("User");
            base.OnModelCreating(modelBuilder);
        }
    }
}
