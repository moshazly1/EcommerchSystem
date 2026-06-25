using BackendEcommerchSystem.DTOs.CartDTO;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) { 
         _cartService = cartService;        
        }
        [HttpGet] 
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); 
            var cart = await _cartService.GetCartAsync(userId); 
            return Ok(cart);        
        }
        [HttpPost("items")] 
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cart = await _cartService.AddToCartAsync(userId, dto);
            return Ok(cart);
        }
        [HttpPut("items/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [FromBody] int quantity)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cart = await _cartService.UpdateQuantityAsync(userId, cartItemId, quantity);
            return Ok(cart);
        }
        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _cartService.RemoveItemAsync(userId, cartItemId);
            return NoContent();
        }
    }
}
