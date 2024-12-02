using AutoMapper;
using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Net;

namespace ElasticSearch.API.Services {
    public class ProductService(ProductRepository productRepository, IMapper mapper,ILogger<ProductService> logger) {
        public async Task<ResponseDto<ProductDto>> SaveProductAsync(ProductCreateDto productCreateDto, CancellationToken cancellationToken) {

            Product? p = await productRepository.SaveAsync(productCreateDto.GetProduct(), cancellationToken);

            if (p is null) {
                return ResponseDto<ProductDto>.Error(new List<string>() { "Bir hata meydana geldi" }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(mapper.Map<ProductDto>(p), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllProducts(CancellationToken cancellationToken) {
            var products = await productRepository.GetAllAsync(cancellationToken);

            return ResponseDto<List<ProductDto>>.Success(mapper.Map<List<ProductDto>>(products), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string Id, CancellationToken cancellationToken) {

            var response = await productRepository.GetByIdAsync(Id, cancellationToken);
            if (response is null) return ResponseDto<ProductDto>.Error("Id not found", HttpStatusCode.NotFound);
            return ResponseDto<ProductDto>.Success(mapper.Map<ProductDto>(response), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> UpdateProductAsync(ProductUpdateDto updatedProduct, CancellationToken cancellationToken) {
            var res = await productRepository.UpdateProduct(updatedProduct, cancellationToken);
            if (!res) return ResponseDto<bool>.Error("An error occured", HttpStatusCode.InternalServerError);
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteProduct(string Id, CancellationToken cancellationToken) {
            var response = await productRepository.DeleteProduct(Id, cancellationToken);

            if(!response.IsValidResponse && response.Result == Result.NotFound) {
                return ResponseDto<bool>.Error("The Id you sent is not exist in our database ", HttpStatusCode.NotFound);
            }


            if (!response.IsValidResponse) {
                Exception exception;
                response.TryGetOriginalException(out exception);

                logger.LogError(exception:exception,message: response.ElasticsearchServerError.Error.ToString());
                return ResponseDto<bool>.Error($"{response.ElasticsearchServerError.Error.ToString()}", HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
    }
}
