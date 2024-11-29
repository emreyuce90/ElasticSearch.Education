namespace ElasticSearch.API.DTOs {
    public record ProductDto {
        public ProductDto() {

        }

        public ProductDto(string id, string name, decimal price, int stock, ProductFeatureDto? features) {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Features = features;
            
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductFeatureDto Features { get; set; }
    }
}
