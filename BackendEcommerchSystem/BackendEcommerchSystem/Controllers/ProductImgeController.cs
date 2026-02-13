using BackendEcommerchSystem.DTOs.ProductImageDTO;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImgeController : ControllerBase
    {
        private readonly IProductImageServices _imgeserv; 
     public    ProductImgeController(IProductImageServices imgeserv)
        {
            _imgeserv = imgeserv;   
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetImagesByProductId(int productId)
        {
            var response = new BaseResponseModel();

            try
            {
                var images = await _imgeserv.GetImagesByProductIdAsync(productId);

                response.Status = true;
                response.StatusMessage = "Images retrieved successfully";
                response.Data = images;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] CreateProductImageDTO dto)
        {
            var response = new BaseResponseModel();

            try
            {
                await _imgeserv.AddImageAsync(dto);

                response.Status = true;
                response.StatusMessage = "Image added successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var response = new BaseResponseModel();

            try
            {
                await _imgeserv.DeleteImageAsync(imageId);

                response.Status = true;
                response.StatusMessage = "Image deleted successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
