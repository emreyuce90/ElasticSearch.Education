using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController(ECommerceRepository repository) : ControllerBase {
        [HttpGet("term-query")]
        public async Task<IActionResult> GetByCustomerId(string customerName) {
            var response = await repository.TermQueryApplication(customerName);
            return Ok(response);
        }
    }
}
