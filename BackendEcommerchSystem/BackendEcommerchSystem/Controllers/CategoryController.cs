using Azure;
using BackendEcommerchSystem.DTOs.CategoryDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Model;
using BackendEcommerchSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetCategory() {
            BaseResponseModel responce = new BaseResponseModel();
            try
            {

                var category = await _categoryService.GetAllAsync();
                if (!category.Any())
                {

                    responce.Status = false;
                    responce.StatusMessage = "No categories found";
                    return NotFound(responce);
                }
                responce.Status = true;
                responce.StatusMessage = "Categories retrieved successfully";
                responce.Data = category;
                return Ok(responce);
            }
            catch (Exception ex) {

                responce.Status = false;
                responce.StatusMessage = "something went wrong ";
                return BadRequest(responce);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByID(int id)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var cat = await _categoryService.GetByIDAsync(id);
                if (cat == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No subcategories found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "Categories retrieved successfully";
                response.Data = cat;
                return Ok(response);
            }
            catch (Exception ex) {

                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory (int id)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                var cat =  await _categoryService.DeleteAsync(id);
                if(cat == null)
                {
                    response.Status = false;
                    response.StatusMessage = "No categories found";
                    return NotFound(response);
                }
                response.Status = true;
                response.StatusMessage = "categories is Deleted";
               response.Data=cat;
                return Ok(response); 
            }
            catch (Exception ex) {
                response.Status = false;
                response.StatusMessage = "something went wrong ";
                return BadRequest(response);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO category)
        {
            BaseResponseModel response = new BaseResponseModel();
            try
            {
                if(!ModelState.IsValid)
                {
                    response.Status = false;
                    response.StatusMessage = "Invalid data";
                    return BadRequest(response);
                }
                var createdCategory = await _categoryService.CreateAsync(category);
                response.Status = true;
                response.StatusMessage = "Category created successfully";
                response.Data = createdCategory;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusMessage = "Something went wrong";
                return BadRequest(response);
            }
        }
  
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDTO dto)
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
                var updatedCategory = await _categoryService.UpdateAsync(id, dto);

                response.Status = true;
                response.StatusMessage = "Category updated successfully";
                response.Data = updatedCategory;

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
