using Amazon.Runtime.Internal;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Security.Claims;
using Task1_Marketplace.Configuration;
using Task1_Marketplace.Domain;
using Task1_Marketplace.Models;

namespace Task1_Marketplace.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoDatabase _db;
        private readonly string _collection;
        private readonly HttpContext _httpContext;
        private IMongoCollection<Product> Products 
        { 
            get 
            { 
                return _db.GetCollection<Product>(_collection); 
            } 
        }
        public ProductService(IHttpContextAccessor contextAccessor, IMongoDatabase client, IOptions<MongoDbConfiguration> config)
        {
            _httpContext = contextAccessor.HttpContext;
            _db = client;
            _collection = config.Value.Collections.Products;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await Products.AsQueryable().ToListAsync();
            return products;
        }

        public async Task<GetProductResponse> GetProductAsync(string id)
        {
            if(!ObjectId.TryParse(id, out var productId))
            {
                return null;
            }
            var product = await Products.AsQueryable().Where(x => id == x.Id).FirstOrDefaultAsync();
            var response = new GetProductResponse()
            {
                Id = product.Id,
                Description = product.Description,
                Image = product.Image,
                AddedBy = new Models.UserInfo()
                {
                    Id = product.AddedBy.Id,
                    Name = product.AddedBy.Name,
                },
                Name = product.Name,
                Price = product.Price,
                Rating = product.Rating
            };
            return response;
        }

        public async Task AddProductAsync(AddProductRequest request)
        {
            var nameIdentifierClaim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var nameClaim = _httpContext.User.FindFirst(ClaimTypes.Name);
            var product = new Product()
            {
                Description = request.Description,
                Image = request.Image,
                Name = request.Name,
                Price = request.Price,
                Rating = request.Rating,
                AddedBy = new Domain.UserInfo()
                {
                    Id = nameIdentifierClaim.Value,
                    Name = nameClaim.Value,
                }
            };

            await Products.InsertOneAsync(product);
        }

        public async Task<List<SearchProductsResult>> SearchProductsAsync(string searchText)
        {
            var filter = Builders<Product>.Filter.Text(searchText);
            var productsFound = await Products.Find(filter).ToListAsync();
            var result = productsFound
                .Select(x => new SearchProductsResult() { Id = x.Id, Name = x.Name })
                .ToList();
            return result;
        }
    }
}
