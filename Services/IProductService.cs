using Task1_Marketplace.Domain;

namespace Task1_Marketplace.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(string id);
    }
}
