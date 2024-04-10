using BankingApp.Models;

namespace BankingApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> AddCustomer(Customer customer)
        {
            return await httpClient.PostAsJsonAsync($"api/Customers", customer);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await httpClient.GetFromJsonAsync<Customer>($"api/Customers/{id}");
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"api/Customers");
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            return await httpClient.DeleteFromJsonAsync<Customer>($"api/Customers/{id}");
        }

        public async Task<HttpResponseMessage> Purchase(int id, decimal amount)
        {
            return await httpClient.PutAsJsonAsync($"api/Customers/purchase/{id}/{amount}",amount);
        }

        public async Task<HttpResponseMessage> Refund(int id, decimal amount)
        {
            return await httpClient.PutAsJsonAsync($"api/Customers/refund/{id}/{amount}", amount);
        }

        public async Task<HttpResponseMessage> TopUpCustomerBalance(int id, decimal amount)
        {
            return await httpClient.PutAsJsonAsync($"api/Customers/topup/{id}/{amount}", amount);
        }
    }
}
