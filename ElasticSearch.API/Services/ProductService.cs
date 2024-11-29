using AutoMapper;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Collections.Generic;
using System.Net;

namespace ElasticSearch.API.Services {
    public class ProductService(ProductRepository productRepository,IMapper mapper) {
        public async Task<ResponseDto<ProductDto>> SaveProductAsync(ProductCreateDto productCreateDto,CancellationToken cancellationToken) {
           
            Product? p  = await productRepository.SaveAsync(mapper.Map<Product>(productCreateDto),cancellationToken);
            
            if(p is null) {
                return ResponseDto<ProductDto>.Error(new List<string>() { "Bir hata meydana geldi"},HttpStatusCode.InternalServerError);
            }
            
            return ResponseDto<ProductDto>.Success(mapper.Map<ProductDto>(p), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllProducts(CancellationToken cancellationToken) {
            var products = await productRepository.GetAllAsync(cancellationToken);
           
            return ResponseDto<List<ProductDto>>.Success(mapper.Map<List<ProductDto>>(products),HttpStatusCode.OK);
        }
    }
}
