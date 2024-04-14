using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.PaymentsApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        //public DbSet<Customer> Customers { get; set; }

    }
}
