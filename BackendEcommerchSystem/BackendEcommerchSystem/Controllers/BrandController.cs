using Azure;
using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.DTOs.BrandDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {

            _context = context;
        }
        [HttpGet]
     
        public async Task<IActionResult> GetAllBrand() {
            BaseResponseModel response = new  BaseResponseModel();      
            try
            {
                var Brand = await _context.Brands.Select(B => new BrandDTO
                {
                    Id = B.Id,
                    Name = B.Name,  

                }).ToListAsync();
                if (!Brand.Any())
                {
                    response.Status = false;
                    response.StatusMessage = "No Brand found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = " Brand retrieved successfully";
                response.Data = Brand; 
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.StatusMessage = "something went wrong "+ ex.Message;
                return BadRequest(response);
            }
         
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandByID (int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var Brand = await _context.Brands.Select(B => new BrandDTO
                {
                    Id = B.Id,
                    Name = B.Name,

                }).FirstOrDefaultAsync(x=>x.Id == id);
                if ( Brand == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No Brand found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = " Prand retrieved successfully";
                response.Data = Brand;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = "something went wrong " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] BrandDTO dto)
        {
            var response = new BaseResponseModel();

            try
            {
                
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    response.Status = false;
                    response.StatusMessage = "Brand name is required";
                    return BadRequest(response);
                }


                bool brandExists = await _context.Brands
                    .AnyAsync(b => b.Name.ToLower() == dto.Name.ToLower());

                if (brandExists)
                {
                    response.Status = false;
                    response.StatusMessage = "Brand name already exists";
                    return BadRequest(response);
                }

                var brand = new Brand
                {
                    Name = dto.Name.Trim()
                };

                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.StatusMessage = "Brand added successfully";
                response.Data = brand;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandDTO dto)
        {
            var response = new BaseResponseModel();

            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    response.Status = false;
                    response.StatusMessage = "Brand name is required";
                    return BadRequest(response);
                }

                // 1️⃣ تحقق من وجود البراند
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    response.Status = false;
                    response.StatusMessage = "Brand not found";
                    return NotFound(response);
                }

                // 2️⃣ تحقق من عدم تكرار الاسم لبراند آخر
                bool nameExists = await _context.Brands
                    .AnyAsync(b => b.Id != id && b.Name.ToLower() == dto.Name.ToLower());

                if (nameExists)
                {
                    response.Status = false;
                    response.StatusMessage = "Brand name already exists";
                    return BadRequest(response);
                }

                // 3️⃣ تحديث البيانات
                brand.Name = dto.Name.Trim();
                await _context.SaveChangesAsync();

                response.Status = true;
                response.StatusMessage = "Brand updated successfully";
                response.Data = brand;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var response = new BaseResponseModel();

            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    response.Status = false;
                    response.StatusMessage = "Brand not found";
                    return NotFound(response);
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.StatusMessage = "Brand deleted successfully";
                response.Data = brand; // ممكن تبعت الاسم أو ID فقط

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
