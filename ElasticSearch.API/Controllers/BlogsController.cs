using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(BlogsRepository repository) : ControllerBase {

        [HttpGet("match-query")]
        public async Task<IActionResult> Get(string title) {
            var result = await repository.MatchQuery(title);
            return Ok(result);
        }

        [HttpGet("match-queryall")]
        public async Task<IActionResult> GetAll() {
            var result = await repository.MatchAllQuery();
            return Ok(result);
        }

        [HttpGet("match-prefix")]
        public async Task<IActionResult> GetAllB(string titleSearch) {
            var result = await repository.MatchPrefixAllQuery(titleSearch);
            return Ok(result);
        }


        [HttpPost("compound-query")]
        public async Task<IActionResult> GetAll(string description) {
            var result = await repository.CompoundQuery(description);
            return Ok(result);
        }

    }
}
