using BankingApp.Domain;

namespace BankingApp.Api.Services
{
    public interface ICustomerService
    {
        Task<Customer> TopUp(int id, decimal amount);
        Task<Customer> Purchase(int id, decimal amount);
        Task<Customer> Refund(int id, decimal amount);
    }
}
