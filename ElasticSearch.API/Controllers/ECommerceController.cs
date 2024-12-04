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
        
        [HttpPost("terms-query")]
        public async Task<IActionResult> GetByCustomers(List<string> customersName) {
            var response = await repository.TermsQuery(customersName);
            return Ok(response);
        }

        [HttpPost("prefix-query")]
        public async Task<IActionResult> GetCustomers(string customerFirstNamePrefix) {
            var response = await repository.PrefixQuery(customerFirstNamePrefix);
            return Ok(response);
        }

        [HttpPost("pagination-query")]
        public async Task<IActionResult> GetCustomersPaginate(int pageSize,int pageNumber) {
            var response = await repository.GetAllWPagination(pageSize,pageNumber);
            return Ok(response);
        }

        [HttpPost("wildcard-query")]
        public async Task<IActionResult> GetCustomersWildCard(string customerName) {
            var response = await repository.GetWildCardQuery(customerName);
            return Ok(response);
        }

        [HttpPost("fuzzy-query")]
        public async Task<IActionResult> GetFuzzyQuery(string customerName) {
            var response = await repository.GetFuzzyQuery(customerName);
            return Ok(response);
        }
    }
}
