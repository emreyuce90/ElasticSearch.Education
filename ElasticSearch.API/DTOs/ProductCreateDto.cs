using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs {
    //IL zamanında class a çevrilir
    // Immutable dır bir kere nesne örneği alındıktan sonra değiştirilemezler
    public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto productFeatureDto) {
       
    }
}
