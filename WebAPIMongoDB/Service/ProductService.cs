using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPIMongoDB.Database;
using WebAPIMongoDB.Models;

namespace WebAPIMongoDB.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Products> _productsCollection;

        public ProductService(IOptions<ProductDataBase> productService){

            MongoClient mongoClient = new MongoClient(productService.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(productService.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Products>
                (productService.Value.ProductCollectionName);
        }


        public async Task<List<Products>> GetAsync() => 
            await _productsCollection.Find(x => true).ToListAsync();

        public async Task<Products> GetProducts(string id) =>
            await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync (Products product) =>
            await _productsCollection.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Products product) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, product);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == id);



    }
}
