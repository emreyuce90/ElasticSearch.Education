using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.Repositories {
    public class BlogsRepository(ElasticsearchClient client) {
        public const string indexName = "blogs";

        public async Task<List<Blog>> MatchQuery(string blogTitle) {

            var result = await client.SearchAsync<Blog>(x => x.Index(indexName).Query(q => q.Match(m => m.Field(f => f.Title).Query(blogTitle))));

            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }

            return result.Documents.ToList();
        }

        public async Task<List<Blog>> MatchAllQuery() {

            var result = await client.SearchAsync<Blog>(x => x.Index(indexName).Size(100).Query(q => q.MatchAll(new MatchAllQuery())));

            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }

            return result.Documents.ToList();
        }

        public async Task<List<Blog>> MatchPrefixAllQuery(string searchName) {

            var result = await client.SearchAsync<Blog>(x => x.Index(indexName).Size(100).Query(q => q.MatchBoolPrefix(m => m.Field(f => f.Title).Query(searchName))));

            foreach (var item in result.Hits) {
                if (item.Source != null)
                    if (item.Id != null)
                        item.Source.Id = item.Id;
            }

            return result.Documents.ToList();
        }

        //Compound Queriler
        //Must and anlamına gelir x ve y olmalı
        //Must Not queryde olmamalı x ve y olmamalı
        //Should or anlamına gelir içerisine aldığı şartları sağlayan x veya y ikisini de getirir
        //Filter

        public async Task<List<Blog>> CompoundQuery(string searchDescription) {
            var result = await client.SearchAsync<Blog>(x => x
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            // Description için match araması (text alanı üzerinde)
                            sh => sh.Match(m => m.Field(f => f.Description).Query(searchDescription)),

                            // Description.keyword için prefix araması (keyword alanı üzerinde)
                            sh => sh.Prefix(pr => pr.Field(f => f.Description.Suffix("keyword")).Value(searchDescription))
                        )
                    )
                )
            );

            foreach (var item in result.Hits) item.Source.Id = item.Id;

            return result.Documents.ToList();
        }



        public async Task<List<Blog>> MatchDescriptionQuery(string searchDescription) {
            // Compound queryleri kullanabilmek için Bool kullanılır
            // Should şu anlama gelir içerisine giren durumları veya şeklinde değerlendirir
            // Title üzerinde kelimeler tam eşleşen indexli kelimeleri getirir +
            // Title üzerinde girilen değer ile başlayan içerikler getirilir
            // Must kullansaydık ikisinin kesiştiği dataları getirecekti
            var result = await client.SearchAsync<Blog>(x => x.Index(indexName)
            .Query(q => q.Match(m => m.Field(f => f.Description).Query(searchDescription))));

            foreach (var item in result.Hits) item.Source.Id = item.Id;

            return result.Documents.ToList();
        }
    }
}
