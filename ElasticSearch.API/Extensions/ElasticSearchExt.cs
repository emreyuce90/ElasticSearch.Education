﻿using Elasticsearch.Net;
using Nest;

namespace ElasticSearch.API.Extensions {
    public static class ElasticSearchExt {
        public static IServiceCollection AddElasticSearchExt(this IServiceCollection services,IConfiguration configuration) {

            var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);

            return services;
        }
    }
}
