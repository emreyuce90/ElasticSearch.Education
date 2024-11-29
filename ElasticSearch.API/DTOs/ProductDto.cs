using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs {
    public record ProductDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductFeatureDto? Feature { get; set; }

        // Parametresiz constructor, AutoMapper için gerekli
        public ProductDto() { }

        // İhtiyaç duyulan constructor (isteğe bağlı)
        public ProductDto(string id, string name, decimal price, int stock, ProductFeatureDto? feature) {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Feature = feature;
        }

       
    }
}
