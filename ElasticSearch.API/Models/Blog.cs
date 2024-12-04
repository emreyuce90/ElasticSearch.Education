using System.Text.Json.Serialization;

namespace ElasticSearch.API.Models {
    public class Blog {
        [JsonPropertyName("_id")] public string Id { get; set; } = default!;
        [JsonPropertyName("title")] public string Title { get; set; } = default!;
        [JsonPropertyName("description")] public string Description { get; set; } = default!;

        
    }
}
