using Azure;
using BackendEcommerchSystem.DTOs.ProductDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using BackendEcommerchSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService) {
            _ProductService = ProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            BaseResponseModel response = new BaseResponseModel();
            try {

                var product = await _ProductService.GetAllProduct();
                if (!product.Any())

                {
                    response.Status = false;
                    response.StatusMessage = "No Product found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Product retrieved successfully";
                response.Data = product;
                return Ok(response);
            } catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }
        [HttpGet("Laptop")]
        public async Task<IActionResult> GetAllLaptop(int page=1 , int pageSize = 8)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var Laptop = await _ProductService.GetAllLaptop(page , pageSize);
                if (!Laptop.Items.Any())

                {
                    response.Status = false;
                    response.StatusMessage = "No Laptop found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Laptop retrieved successfully";
                response.Data = Laptop;
                return Ok(response);

            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }

        [HttpGet("PCs")]
        public async Task<IActionResult> GetAllPCs(int page = 1, int pageSize = 8)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var PCs = await _ProductService.GetAllPCs(page , pageSize);
                if (!PCs.Items.Any())

                {
                    response.Status = false;
                    response.StatusMessage = "No PCs found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "PCs retrieved successfully";
                response.Data = PCs;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.ToString();
                return BadRequest(response);
            }
        }



        [HttpGet("Mice")]
        public async Task<IActionResult> GetAllMice(int page = 1, int pageSize = 8)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var Mice = await _ProductService.GetAllMice(page ,pageSize);
                if (!Mice.Items.Any())

                {
                    response.Status = false;
                    response.StatusMessage = "No Mice found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Mice retrieved successfully";
                response.Data = Mice;
                return Ok(response);

            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }

        [HttpGet("Accessories")]
        public async Task<IActionResult> GetAllAccessories(int page = 1, int pageSize = 8)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var Accessories = await _ProductService.GetAllAccessories(page ,pageSize);
                if (!Accessories.Items.Any())

                {
                    response.Status = false;
                    response.StatusMessage = "No Accessories found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Accessories retrieved successfully";
                response.Data = Accessories;
                return Ok(response);

            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {

                var product = await _ProductService.GetProductByID(id);
                if (product == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No Product found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Product retrieved successfully";
                response.Data = product;
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO product)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.StatusMessage = "Invalid data";
                    return BadRequest(response);
                }
                var Product = await _ProductService.AddProduct(product);
                response.Status = true;
                response.StatusMessage = "Product created successfully";
                response.Data = Product;
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "Something went wrong";
                return BadRequest(response);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id , UpdateProductDTO Product) {
            BaseResponseModel response = new BaseResponseModel () ;
            try {
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.StatusMessage = "Invalid data";
                    return BadRequest(response);
                }
                var pro = await _ProductService.UpdateProduct(id, Product);
                response.Status = true;
                response.StatusMessage = "Product updated successfully";
                response.Data = pro;
                return Ok(response);    
            }
            catch (KeyNotFoundException ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return NotFound(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "Something went wrong";
                return BadRequest(response);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) { 
        BaseResponseModel response = new BaseResponseModel () ;  
            try { 
              await _ProductService.DeleteProduct(id);  
               
                   
             
                response.Status = true;
                response.StatusMessage = "Product deleted successfully";
               
                return Ok(response);
            }
            catch
            {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }            
    }
}
