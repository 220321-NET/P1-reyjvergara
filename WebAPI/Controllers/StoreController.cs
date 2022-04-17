using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// All this is a class with HTTP centered methods
namespace WebAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // If this IFABL does not somehow work despite given the scope, use FABL instead
        private readonly IFABL _bl;
        public StoreController(IFABL bl)
        {
            _bl = bl;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public List<StoreFront> Get()
        {
            return _bl.GetStoreFronts();
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<StoreFront> Get(int id)
        {
            StoreFront? storeToReturn = _bl.GetStoreByID(id);
            if (storeToReturn != null)
            {
                return Ok(storeToReturn);
            }
            return NoContent();
        }

        // POST api/<StoreController>
        // Model validation
        [HttpPost]
        public ActionResult<StoreFront> Post([FromBody] StoreFront storeToCreate)
        {
            return Created("api/Store", _bl.CreateStore(storeToCreate));
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
