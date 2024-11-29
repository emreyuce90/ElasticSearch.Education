using AutoMapper;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.Mapping {
    public class ProductMapping:Profile {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>();
            CreateMap<ProductCreateDto, Product>()
               .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => src.Feature)) // Feature'ı mapleme
               .ReverseMap();
        }
    }
}
