using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.PaymentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // must delete
    public class PaymentsController : ControllerBase
    {
        private const string BaseUrl = "https://localhost:7249";
        private readonly HttpClient _httpClient;

        public PaymentsController()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl)};
        }


        [HttpPut("topup/{id:int}/{amount:decimal}")]
        public async Task<ActionResult<Customer>> TopUpCustomerBalance(int id, decimal amount)
        {
            try
            {
                var customer = _httpClient.GetFromJsonAsync<Customer>($"/api/Customers/{id}");

                if (customer == null)
                {
                    return NotFound($"Customer with Id = {customer.Id} not found !");
                }

                //customer = await customerRepository.TopUp(id, amount);
                return Ok(customer);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }
    }
}
