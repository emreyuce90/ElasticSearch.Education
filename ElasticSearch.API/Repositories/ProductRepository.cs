using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.Repositories {
    public class ProductRepository(ElasticsearchClient elasticClient) {
        private  const string indexName = "products";
        public async Task<Product?> SaveAsync(Product newProduct,CancellationToken cancellationToken) {
            newProduct.CreatedDate = DateTime.Now;
            var response = await elasticClient.IndexAsync(newProduct, x => x.Index(indexName).Id(Guid.NewGuid().ToString()),cancellationToken);
            if(!response.IsValidResponse) return null;
            newProduct.Id = response.Id;
            return newProduct;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken) {
           var list =  await elasticClient.SearchAsync<Product>(search =>search.Index(indexName));

            foreach (var hit in list.Hits)
            {
                hit.Source.Id = hit.Id;
            }
            return list.Documents.ToList();
        }

        public async Task<Product?> GetByIdAsync(string Id,CancellationToken cancellationToken) {
            var product = await elasticClient.GetAsync<Product>(Id, x => x.Index(indexName));

            if (!product.IsValidResponse) {
                return null;
            }
            product.Source.Id = product.Id;
            return product.Source;
        }

        public async Task<bool> UpdateProduct(ProductUpdateDto updatedProduct,CancellationToken cancellationToken) {
            var data = await elasticClient.UpdateAsync<Product, ProductUpdateDto>(updatedProduct.Id, x => x.Index(indexName).Doc(updatedProduct),cancellationToken);
            return data.IsValidResponse;
        }

        public async Task<DeleteResponse> DeleteProduct(string Id,CancellationToken cancellationToken) {
            return await elasticClient.DeleteAsync<Product>(Id,x=>x.Index(indexName));
             
        }
    }
}
