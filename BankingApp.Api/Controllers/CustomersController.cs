using BankingApp.Api.Models;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                return Ok(await customerRepository.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var customer = await customerRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                if(customer == null)
                {
                    return BadRequest();
                }

                var addedCustomer = await customerRepository.AddCustomer(customer);

                return CreatedAtAction(nameof(GetCustomer), new { id = addedCustomer.Id }, addedCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data into the database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                var result = await customerRepository.GetCustomerById(id);
                
                if(result == null)
                {
                    return NotFound($"Customer not found in this {id}");
                }

                return Ok(await customerRepository.DeleteCustomer(id));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data !");
            }
        }

        [HttpPut("topup/{id:int}/{amount:decimal}")]
        public async Task<ActionResult<Customer>> TopUpCustomerBalance(int id, decimal amount)
        {
            try
            {
                var result = await customerRepository.GetCustomerById(id);

                if (result == null)
                {
                    return NotFound($"Customer with Id = {result.Id} not found !");
                }

                result = await customerRepository.TopUp(id, amount);
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }

        [HttpPut("refund/{id:int}/{amount:decimal}")]
        public async Task<ActionResult<Customer>> RefundCustomerBalance(int id, decimal amount)
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

                customer = await customerRepository.Refund(id, amount);
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }

        [HttpPut("purchase/{id:int}/{amount:decimal}")]
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

                customer = await customerRepository.Purchase(id, amount);
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data !!!");
            }
        }

    }
}
