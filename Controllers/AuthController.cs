using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Task1_Marketplace.Services;
using Task1_Marketplace.Models;

namespace Task1_Marketplace.Controllers
{
    public record Response(bool IsSuccess, string Message);
    public record UserClaim(string Type, string Value);


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;  
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                await _userService.RegisterAsync(registerRequest);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            await _userService.SignInAsync(signInRequest);

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
