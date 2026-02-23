using JwtAuthDotNet.DTO;
using JwtAuthDotNet.Entities;
using JwtAuthDotNet.Servise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public readonly IAuthService _authService; 
        public    AuthController (IConfiguration configuration , IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;     
        }
    

        [HttpPost("register")] 
        public async Task<ActionResult<User>> Register(UserDTO  request) 
        {
            var  user= await _authService.RegisterAsync(request);
            if(user == null )
            {
                return BadRequest("Username alread exists.");      
            }
            return Ok(user);
            
        }
        [HttpPost("Login")] 
        public async Task<ActionResult<string>> Login (UserDTO request)
        {
            var token = await _authService.LoginAysnc(request);
            if(token == null )
            {
                return BadRequest("Invalid userName or Password");
            }
            return Ok(token); 
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthanticatedOnlyEndPoint()
        {
            return Ok("You Are Authanticated!");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndPoint()
        {
            return Ok("You Are admin!");
        }
    }
}
