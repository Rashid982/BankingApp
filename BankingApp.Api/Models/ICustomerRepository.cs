using BankingApp.Domain;

namespace BankingApp.Api.Models
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
        Task<Customer> UpdateCostumer(Customer customer);
        
    }
}
