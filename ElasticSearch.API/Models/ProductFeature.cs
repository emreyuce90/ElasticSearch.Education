namespace ElasticSearch.API.Models {
    public class ProductFeature {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public Color? Color { get; set; }

        public ProductFeature(int? width,int?height,Color? color)
        {
            Width = width; Height = height; Color = color;
        }
    }
}
