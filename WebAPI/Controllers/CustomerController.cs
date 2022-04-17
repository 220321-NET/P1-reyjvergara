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

        [HttpGet]
        public List<Customer> Get()
        {
            return _bl.GetAllCustomers();
        }
        [HttpGet("Find Customer")]
        public ActionResult<Customer> Get(string email, string password)
        {
            Customer customerToReturn = _bl.FindCustomer(email, password);
            if(customerToReturn != null)
            {
                return Ok(customerToReturn);
            }
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customerToCreate)
        {
            return Created("api/Customer", _bl.CreateCustomer(customerToCreate));
        }
    }
}
