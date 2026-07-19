using BackendEcommerchSystem.DTOs.WhiteListDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WhiteListController : ControllerBase
    {
        private readonly IWhiteListServices _whiteListServices;
        public WhiteListController(IWhiteListServices whiteListServices)
        {
            _whiteListServices = whiteListServices;     
        }
        [HttpGet("WhiteList")]
        public async Task<IActionResult> GetAllWhiteLisItemAysnc()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User is not authenticated.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user id.");

            try
            {
                var result = await _whiteListServices.GetWhiteListaysnc(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("AddWhiteList")]
        public async Task<IActionResult> AddWhiteListAsync([FromBody] AddWhiteListDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid user id.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _whiteListServices.AddAsync(userId, dto);

            return Ok(new
            {
                Message = "Product added to wishlist successfully."
            });
        }
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveWhiteListAsync([FromBody] AddWhiteListDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid user id.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _whiteListServices.RemoveAsync(userId, dto);

            return Ok(new
            {
                Message = "Product removed from wishlist successfully."
            });
        }
    }
}
