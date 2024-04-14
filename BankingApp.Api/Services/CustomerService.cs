using BankingApp.Domain;
using BankingApp.Domain.Database;

namespace BankingApp.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext appDbContext;

        public CustomerService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Customer> Purchase(int id, decimal amount)
        {
            var customer = appDbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                if (customer.Balance >= amount)
                {
                    customer.Balance -= amount;
                    await appDbContext.SaveChangesAsync();
                    return customer;
                }
            }

            return null;
        }

        public async Task<Customer> Refund(int id, decimal amount)
        {
            var customer = appDbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {                
                 customer.Balance += amount;
                 await appDbContext.SaveChangesAsync();
                 return customer;        
            }

            return null;
        }

        public async Task<Customer> TopUp(int id, decimal amount)
        {
            var customer = appDbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                customer.Balance += amount;
                await appDbContext.SaveChangesAsync();
                return customer;
            }

            return null;
        }
    }
}
