using ElasticSearch.API.DTOs;
using ElasticSearch.API.Extensions;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ProductService productService) : ControllerBase {
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto,CancellationToken cancellationToken) {

            var response = await productService.SaveProductAsync(productCreateDto,cancellationToken);
            return response.ToGenericResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken) {
            var response = await productService.GetAllProducts(cancellationToken);
            return response.ToGenericResult();
        }
    }
}
