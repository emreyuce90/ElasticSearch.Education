using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.Models;
using ElasticSearch.API.Models.ECommerce;

namespace ElasticSearch.API.Repositories {
    public class ECommerceRepository(ElasticsearchClient client) {

        public const string indexName = "kibana_sample_data_ecommerce";
        public async Task<List<ECommerce>> TermQueryApplication(string customerName) {

            var query = await client.SearchAsync<ECommerce>(x => x.Index(indexName)
            .Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerName))));


            foreach (var item in query.Hits)
            {
                item.Source.Id = item.Id;
            }
            return query.Documents.ToList();
        }
    }
}
