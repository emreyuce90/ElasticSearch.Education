using AutoMapper;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Net;

namespace ElasticSearch.API.Services {
    public class ProductService(ProductRepository productRepository, IMapper mapper) {
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
    }
}
