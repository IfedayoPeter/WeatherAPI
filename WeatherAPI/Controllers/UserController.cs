using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherAPI.services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(UserLogin user)
        {
            var token = _userService.Login(user);
            if (string.IsNullOrWhiteSpace(token)) return NotFound("User not found");
            return Ok(new Dictionary<string, string> { { "Token", token } });          
        }
    }
}
