using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFABL _bl;

        public ProductController(IFABL bl)
        {
            _bl = bl;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<List<Product>> GetAsync()
        {
            return await _bl.GetAllProductsAsync();
        }

        // GET api/<ProductController>/5
        [HttpGet("FindWithStoreID")]
        public async Task<List<Product>> GetAsync(int storeId)
        {
            return await _bl.GetStoreProductsAsync(storeId);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
