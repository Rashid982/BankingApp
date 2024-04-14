using BankingApp.Domain;

namespace BankingApp.Services
{
    public interface ICustomerService
    {
        Task<HttpResponseMessage> AddCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<HttpResponseMessage> TopUpCustomerBalance(int id, decimal amount);
        Task<HttpResponseMessage> Purchase(int id, decimal amount);
        Task<HttpResponseMessage> Refund(int id, decimal amount);
    }
}
