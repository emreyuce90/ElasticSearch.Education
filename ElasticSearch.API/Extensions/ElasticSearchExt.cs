using Elastic.Clients.Elasticsearch;

namespace ElasticSearch.API.Extensions {
    public static class ElasticSearchExt {
        public static IServiceCollection AddElasticSearchExt(this IServiceCollection services,IConfiguration configuration) {

            var client = new ElasticsearchClient(new Uri(configuration.GetSection("Elastic")["Url"]!));
            services.AddSingleton(client);
            return services;
        }
    }
}
