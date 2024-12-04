using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models;
using ElasticSearch.API.Models.ECommerce;
using Microsoft.AspNetCore.Identity;

namespace ElasticSearch.API.Repositories {
    public class ECommerceRepository(ElasticsearchClient client) {
        public const string indexName = "kibana_sample_data_ecommerce";

        public async Task<List<ECommerce>> TermQueryApplication(string customerName) {
            var query = await client.SearchAsync<ECommerce>(
                x => x
                .Index(indexName)
                .Query(q => q
                .Term(t => t
                .Field(f => f.CustomerFirstName.Suffix("keyword"))
                .Value(customerName))));


            foreach (var item in query.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }

            return query.Documents.ToList();
        }

        public async Task<List<ECommerce>> TermsQuery(List<string> customerName) {
            //customer listesini FieldValue a mapledik
            List<FieldValue> terms = new List<FieldValue>();

            foreach (var item in customerName) {
                terms.Add(item);
            }


            var result = await client.SearchAsync<ECommerce>(x => x.Index(indexName).Query(q => q.Terms(t => t.Field(f => f.CustomerFirstName.Suffix("keyword")).Terms(new TermsQueryField(terms)))));


            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }
            return result.Documents.ToList();
        }

        public async Task<List<ECommerce>> PrefixQuery(string customerFirstNamePrefix) {
            
            var result = await client.SearchAsync<ECommerce>(
                x => x.Index(indexName)
                .Query(q => q
                .Prefix(p => p
                .Field(f => f.CustomerFirstName.Suffix("keyword")).Value(customerFirstNamePrefix)
                )));

            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }



            return result.Documents.ToList();
        }

        public async Task<List<ECommerce>> GetAllWPagination(int pageSize,int from) {
            var result = await client.SearchAsync<ECommerce>(x => x
            .Index(indexName)
            .Size(pageSize).From((from-1)*pageSize)
            .Query(q=>q
            .MatchAll(new MatchAllQuery())));

            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
            return result.Documents.ToList();
        }

        public async Task<List<ECommerce>> GetWildCardQuery(string search) {
            var result = await client.SearchAsync<ECommerce>(x => x
            .Index(indexName)
            .Query(q => q
            .Wildcard(w => w
            .Field(f => f.CustomerFirstName.Suffix("keyword")).Wildcard(search))));

            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }
            return result.Documents.ToList();
        }
        
        public async Task<List<ECommerce>> GetFuzzyQuery(string customerName) {
            
            var result = await client.SearchAsync<ECommerce>(x => x.Index(indexName).Query(q => q.Fuzzy(f => f.Field(fi => fi.CustomerFirstName.Suffix("keyword")).Value(customerName))));
           
            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }
            return result.Documents.ToList();
        }


        //Pagination, Gender , StartDate ,EndDate ,FullName ,Category

        public async Task<(List<ECommerce> list ,long totalCount)> GetComplexQuery(SearchViewModel request) {
           //Bu nesne direkt olarak clienta verilir
            List<Action<QueryDescriptor<ECommerce>>> listQuery = new();


            //gender koşulu eklendi male or female exact match
            if (!String.IsNullOrEmpty(request.Gender)) {
                listQuery.Add(x => x.Term(m => m.Field(f => f.Gender.Suffix("keyword")).Value(request.Gender)));
            }

            //customer fullname ad veya soyad
            if (!String.IsNullOrEmpty(request.CustomerFullName)) {
                listQuery.Add(x => x.Match(m => m.Field(f => f.Gender).Query(request.CustomerFullName)));
            }

            //categoryname tam eşleşme
            if (!String.IsNullOrEmpty(request.CategoryName)) {
                listQuery.Add(x => x.Term(m => m.Field(f => f.Gender.Suffix("keyword")).Value(request.CategoryName)));
            }

            if (request.StartDate.HasValue) {

                listQuery.Add(item => item.Range(r => r.DateRange(dr => dr.Field(f => f.OrderDate).Gte(request.StartDate.Value))));
            }

            if (request.StartDate.HasValue) {

                listQuery.Add(item => item.Range(r => r.DateRange(dr => dr.Field(f => f.OrderDate).Lte(request.EndDate.Value))));
            }


            //Girilen tüm şartlar must olmalı 
           var result = await client.SearchAsync<ECommerce>(x => x.Index(indexName).Size(request.PageSize).From((request.CurrentPage - 1)*request.PageSize).Query(q=>q.Bool(b=>b.Must(listQuery.ToArray()))));
            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }

            return (result.Documents.ToList(),result.Total);

        }
    }
}