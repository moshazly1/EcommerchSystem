using BackendEcommerchSystem.DTOs.CategoryDTO;
using BackendEcommerchSystem.DTOs.SubCategoryDTO;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using BackendEcommerchSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
     
        public async Task<IActionResult> GetAll()
        {
            BaseResponseModel responce = new BaseResponseModel();
            try
            {
                var subCategory = await _subCategoryService.GetAllAsync();
                if (!subCategory.Any())
                {
                    responce.Status = false;
                    responce.StatusMessage = "No subcategories found";
                    return NotFound(responce);
                }
                responce.Status = true;
                responce.StatusMessage = "SubCategories retrieved successfully";
                responce.Data = subCategory;
                return Ok(responce);
            }
            catch
            {
                responce.Status = false;
                responce.StatusMessage = "something went wrong ";
                return BadRequest(responce);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var subcat = await _subCategoryService.GetByIdAsync(id);
                if (subcat == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No subcategories found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "subcategories retrieved successfully";
                response.Data = subcat;
                return Ok(response);
            }
            catch 
            {

                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var cat = await _subCategoryService.DeleteAsync(id);
                if (cat == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No subcategories found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "SubCategories is Deleted";
                response.Data = cat;
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
        public async Task<IActionResult> CreateSubCategory(SubCreateDTO category)
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
                var createdSubCategory = await _subCategoryService.CreateAsync(category);
                response.Status = true;
                response.StatusMessage = "SubCategory created successfully";
                response.Data = createdSubCategory;
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
        public async Task<IActionResult> UpdateSubCategory(int id, UpdateSubCategoryDTO dto)
        {
            BaseResponseModel response = new BaseResponseModel();

            if (!ModelState.IsValid)
            {
                response.Status = false;
                response.StatusMessage = "Invalid data";
                return BadRequest(response);
            }

            try
            {
                var updatedSubCategory = await _subCategoryService.UpdateAsync(id, dto);
                if (updatedSubCategory == null)
                {
                    response.Status = false;
                    response.StatusMessage = "SubCategory not found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "SubCategory updated successfully";
                response.Data = updatedSubCategory;

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

    }
}
