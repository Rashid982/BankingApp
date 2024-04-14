using BankingApp.Domain;
using BankingApp.Domain.Database;
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

        public async Task<Customer> UpdateCostumer(Customer customer)
        {
            var _customer = appDbContext.Customers.FirstOrDefault(x => x.Id == customer.Id);

            if (_customer != null)
            {
                _customer.Name = customer.Name;
                _customer.Surname = customer.Surname;
                _customer.BirthDate = customer.BirthDate;
                _customer.PhoneNumber = customer.PhoneNumber;
                _customer.Balance = customer.Balance;

                await appDbContext.SaveChangesAsync(); 

                return _customer;
            }

            return null;
        }
    }
}
