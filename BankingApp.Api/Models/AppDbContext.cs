using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
        }
         
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                Name = "Rashid",
                Surname = "Aliyev",
                PhoneNumber = "51 980 08 69",
                BirthDate = new DateTime(1996, 08, 31),
                Balance = 50
            });
        }
    }
}

