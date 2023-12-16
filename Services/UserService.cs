using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Task1_Marketplace.Configuration;
using Task1_Marketplace.Domain;
using Task1_Marketplace.Models;
using MongoDB.Driver.Linq;
using Task1_Marketplace.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;

namespace Task1_Marketplace.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoDatabase _db;
        private readonly string _collection;
        private readonly HttpContext _httpContext;
        private IMongoCollection<User> Users
        {
            get
            {
                return _db.GetCollection<User>(_collection);
            }
        }
        public UserService(IHttpContextAccessor contextAccessor, IMongoDatabase client, IOptions<MongoDbConfiguration> config)
        {
            _httpContext = contextAccessor.HttpContext;
            _db = client;
            _collection = config.Value.Collections.Users;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await Users.AsQueryable().Where(x => request.Email == x.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists");
            }

            (string salt, string hash) = PasswordHasher.HashPassword(request.Password);
            var user = new User()
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = hash,
                PasswordSalt = salt,
            };
            await Users.InsertOneAsync(user);
        }

        public async Task SignInAsync(SignInRequest request)
        {
            var existingUser = await Users.AsQueryable().Where(x => request.Email == x.Email).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new InvalidOperationException("User does not exists");
            }

            bool isPasswordValid = PasswordHasher.ValidatePassword(request.Password, existingUser.PasswordSalt, existingUser.PasswordHash);
            if(!isPasswordValid)
            {
                throw new InvalidOperationException("Invalid password");
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.NameIdentifier, value: existingUser.Id.ToString()),
                new Claim(type: ClaimTypes.Email, value: existingUser.Email),
                new Claim(type: ClaimTypes.Name,value: existingUser.Name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });
        }
    }
}
