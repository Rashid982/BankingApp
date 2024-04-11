using BankingApp.Api.Controllers;
using BankingApp.Api.Models;
using BankingApp.Models;
using FakeItEasy;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BankingApp.UnitTest
{
    public class UnitTest
    {
        private const string BaseUrl = "https://localhost:7249"; // Update with your API base URL
        private readonly HttpClient _httpClient;

        public UnitTest()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
        }

        [Fact]
        public async Task Test_TopUpMethod()
        {
            await _httpClient.PutAsJsonAsync($"api/Customers/topup/{1}/{20}", 20.0m);

            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{1}");

            Assert.Equal(50.0m, customer.Balance);       
        }

        [Fact]
        public async Task Test_PurchaseMethod()
        {
            await _httpClient.PutAsJsonAsync($"api/Customers/purchase/{1}/{10}", 10);

            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{1}");

            Assert.Equal(20.0m, customer.Balance);
        }

        [Fact]
        public async Task Test_RefundMethod()
        {
            await _httpClient.PutAsJsonAsync($"api/Customers/refund/{1}/{5}", 5);

            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/Customers/{1}");

            Assert.Equal(40.0m, customer.Balance);
        }
    }
}