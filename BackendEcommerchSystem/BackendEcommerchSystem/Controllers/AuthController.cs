using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IAuthService _authService;        

        public AuthController(IAuthService authService)
        {
            _authService = authService; 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);     
            }
            var result = await _authService.RegisterAsync(model);
          if (!result.IsAuthentication)
            {
                return BadRequest(result.Mesage);  
            }
          return Ok(result);  
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Logen([FromBody]  LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.LoginAsync(model);
            if (!result.IsAuthentication)
            {
                return BadRequest(result.Mesage);
            }
            return Ok(result);
        }
    }
}
