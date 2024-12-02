using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Text.Json.Serialization;

namespace ElasticSearch.API.Models.ECommerce {
    public class ECommerce {
        [JsonPropertyName("_id")] public string Id { get; set; } = default!;
        [JsonPropertyName("category")] public string[] Category { get; set; } = default!;
        [JsonPropertyName("customer_first_name")] public string CustomerFirstName { get; set; } = default!;
        [JsonPropertyName("customer_last_name")] public string CustomerLastName { get; set; } = default!;
        [JsonPropertyName("customer_full_name")] public string CustomerFullName { get; set; } = default!;
        [JsonPropertyName("order_id")] public int OrderId { get; set; } = default!;
        [JsonPropertyName("order_date")]public DateTime OrderDate { get; set; } = default!;
        [JsonPropertyName("products")] public Product[] Products { get; set; } = default!;
    }

    public class Product {
        [JsonPropertyName("product_id")] public long Id { get; set; } = default!;
        [JsonPropertyName("product_name")] public string Name { get; set; } = default!;
    }
}
