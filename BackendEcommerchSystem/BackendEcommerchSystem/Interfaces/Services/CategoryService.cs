using BackendEcommerchSystem.DTOs.CategoryDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public class CategoryService : ICategoryService
    { 
        private readonly ICategoryRepository _categoryRepository; 

         public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;   
        }
        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                ParentId = dto.ParentId
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ParentId = category.ParentId
            };
        }


        public async Task<CategoryDTO> DeleteAsync(int id)
        {
            var cat = _categoryRepository.GetByIdAsync(id); 
            if( cat == null )
            {
                throw new Exception($" the: {id}Not Fond"); 
            }
            await _categoryRepository.DeleteAsync(id); 
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDTO
            {

                CategoryId = cat.Id,
            }; 

            
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            
        }

        public Task<CategoryDTO> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
