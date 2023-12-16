using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.ComponentModel;
using Task1_Marketplace.Configuration;
using Task1_Marketplace.Domain;

namespace Task1_Marketplace.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoDatabase _db;
        private readonly string _collection;
        private IMongoCollection<Product> Products 
        { 
            get 
            { 
                return _db.GetCollection<Product>(_collection); 
            } 
        }
        public ProductService(IMongoDatabase client, IOptions<MongoDbConfiguration> config)
        {
            _db = client;
            _collection = config.Value.Collections.Products;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await Products.AsQueryable().ToListAsync();
            return products;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            if(ObjectId.TryParse(id, out var productId))
            {
                return null;
            }
            var product = await Products.AsQueryable().Where(x => id == x.Id).FirstOrDefaultAsync();
            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            //return null;
        }
    }
}
