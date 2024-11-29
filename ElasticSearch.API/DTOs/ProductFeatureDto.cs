
using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs {
    public record ProductFeatureDto {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public Color? Color { get; set; }

        // Parametresiz constructor
        public ProductFeatureDto() { }

        // İsteğe bağlı constructor
        public ProductFeatureDto(int? width, int? height, Color? color) {
            Width = width;
            Height = height;
            Color = color;
        }
    }
}

