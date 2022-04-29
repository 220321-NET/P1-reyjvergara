using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

namespace WebAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFABL _bl;

        public CustomerController(IFABL bl)
        {
            _bl = bl;
        }


        [HttpGet("GetAll")]
        public async Task<List<Customer>> GetAsync()
        {
            return await _bl.GetAllCustomersAsync();
        }

        [HttpGet("FindEmail/{email}")]
        public async Task<Customer> GetAsync(string email)
        {
            return await _bl.FindCustomerByEmailAsync(email);
        }

        [HttpGet("Find/{email}/{password}")]
        public async Task<ActionResult<Customer>> GetAsync(string email, string password)
        {
            Customer customerToReturn = await _bl.FindCustomerAsync(email, password);
            if(customerToReturn != null)
            {
                return Ok(customerToReturn);
            }
            return NoContent();
        }

        [HttpPost("Create/{customerToCreate}")]
        public ActionResult<Customer> Post([FromBody] Customer customerToCreate)
        {
            return Created("api/Customer", _bl.CreateCustomer(customerToCreate));
        }
    }
}
