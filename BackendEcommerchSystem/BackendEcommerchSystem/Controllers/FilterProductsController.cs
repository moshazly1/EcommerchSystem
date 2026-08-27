using BackendEcommerchSystem.DTOs.FiltrationDTO;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterProductsController : ControllerBase
    {

        private readonly IProductService _ProductService;
        public FilterProductsController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        [HttpGet("filter")]

        public async Task<IActionResult> FilterProdcts([FromQuery] ProductFilterDTO dto)
        {
            var result = await _ProductService.GetFilteredProductsAsync(dto); 
            return Ok(result);
        }
    }
}
