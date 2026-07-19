using BackendEcommerchSystem.DTOs.OrderDTO;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendEcommerchSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        // POST /api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                dto.UserId = userId;
                await _orderServices.CreateOrderAsync(dto);
                return StatusCode(201, "Order created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/order/my-orders
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var orders = await _orderServices.GetOrdersByUserId(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderServices.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT /api/order/status
        [HttpPut("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusDTO dto)
        {
            try
            {
                await _orderServices.UpdateOrderStatus(dto);
                return Ok("Order status updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}