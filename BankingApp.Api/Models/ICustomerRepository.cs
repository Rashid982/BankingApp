using BankingApp.Models;

namespace BankingApp.Api.Models
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
        Task<Customer> TopUp(int id, decimal amount);
        Task<Customer> Purchase(int id, decimal amount);
        Task<Customer> Refund(int id, decimal amount);
    }
}
