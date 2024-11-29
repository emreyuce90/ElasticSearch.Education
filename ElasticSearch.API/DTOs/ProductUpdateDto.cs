namespace ElasticSearch.API.DTOs {
    public record ProductUpdateDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductFeatureDto? Feature { get; set; }


    }
}
