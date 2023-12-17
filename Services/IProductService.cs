using Task1_Marketplace.Domain;
using Task1_Marketplace.Models;

namespace Task1_Marketplace.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<GetProductResponse> GetProductAsync(string id);
        Task AddProductAsync(AddProductRequest request);
    }
}
