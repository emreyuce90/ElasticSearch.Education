using ElasticSearch.API.Models;
using Nest;

namespace ElasticSearch.API.DTOs {
    //IL zamanında class a çevrilir
    // Immutable dır bir kere nesne örneği alındıktan sonra değiştirilemezler
    public record ProductCreateDto {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductFeatureDto? Feature { get; set; }

        // Parametresiz constructor, AutoMapper için gerekli
        public ProductCreateDto() { }

        // İhtiyaç duyulan constructor (isteğe bağlı)
        public ProductCreateDto(string name, decimal price, int stock, ProductFeatureDto? feature) {
            Name = name;
            Price = price;
            Stock = stock;
            Feature = feature;
        }

        public Product GetProduct() {
            if (Feature is null) return new Product { Name = Name, Price = Price, Stock = Stock };
            return new Product { Name = Name, Price = Price, Stock = Stock, Feature = new ProductFeature(Feature.Width, Feature.Height, Feature.Color) };
        }
    }
}
