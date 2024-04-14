using BankingApp.Api.Models;
using BankingApp.Api.Services;
using BankingApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ICustomerRepository customerRepository;

        public PaymentsController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            this.customerService = customerService;
            this.customerRepository = customerRepository;
        }


        [HttpPatch("topup/{topup}")] 
        public async Task<ActionResult<Customer>> TopUpCustomerBalance(int id, decimal amount)
        {
            try
            {
                var result = await customerRepository.GetCustomerById(id);

                if (result == null)
                {
                    return NotFound($"Customer with Id = {result.Id} not found !");
                }

                result = await customerService.TopUp(id, amount);
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }

        [HttpPatch("refund/{refund}")]
        public async Task<ActionResult<Customer>> RefundCustomerBalance(int id, decimal amount) 
        {
            try
            {
                var customer = await customerRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound($"Customer with Id = {customer.Id} not found !");
                }
                                
                customer = await customerService.Refund(id, amount);
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }

        [HttpPatch("purchase/{purchase}")]
        public async Task<ActionResult<Customer>> PurchaseCustomerBalance(int id, decimal amount)
        {
            try
            {
                var customer = await customerRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound($"Customer with Id = {customer.Id} not found !");
                }

                if (customer.Balance < amount)
                {
                    return BadRequest("Insufficient balance");
                }

                customer = await customerService.Purchase(id, amount);
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }
    }
}
