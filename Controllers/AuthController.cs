using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Task1_Marketplace.Controllers
{
    public record SignInRequest(string Email, string Password);
    public record Response(bool IsSuccess, string Message);
    public record UserClaim(string Type, string Value);
    public record User(string Email, string Name, string Password);


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private List<User> users = new()
        {
            new("user1@test.com", "User 1", "user1"),
            new("user2@test.com", "User 2", "user2"),
        };

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            var user = users.FirstOrDefault(x => x.Email == signInRequest.Email &&
                                                x.Password == signInRequest.Password);
            if (user is null)
            {
                return BadRequest(new Response(false, "Invalid credentials."));
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: signInRequest.Email),
                new Claim(type: ClaimTypes.Name,value: user.Name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return Ok(new Response(true, "Signed in successfully"));
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();

            return Ok(userClaims);
        }

        [Authorize]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
