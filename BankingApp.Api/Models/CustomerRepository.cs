using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Api.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext appDbContext;
        public CustomerRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await appDbContext.Customers.AddAsync(customer);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = appDbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                appDbContext.Customers.Remove(customer);
                await appDbContext.SaveChangesAsync();
                return customer;
            }

            return null;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await appDbContext.Customers.ToListAsync();
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

            if(customer != null)
            {
                if(customer.Balance >= amount)
                {
                    customer.Balance -= amount;
                    await appDbContext.SaveChangesAsync();
                    return customer;
                }
                
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
