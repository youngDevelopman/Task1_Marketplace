using Task1_Marketplace.Models;

namespace Task1_Marketplace.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterRequest request);
        Task SignInAsync(SignInRequest request);
    }
}
