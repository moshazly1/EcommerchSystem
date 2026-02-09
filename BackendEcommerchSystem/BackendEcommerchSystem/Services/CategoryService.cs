using BackendEcommerchSystem.DTOs.CategoryDTO;
using BackendEcommerchSystem.DTOs.SubCategoryDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class CategoryService : ICategoryService
    { 
        private readonly ICategoryRepository _categoryRepository; 
         public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;   
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            return category.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                SubCategories = c.SubCategories.Select(sc => new SubCategoryDTO
                {
                    SubCategoryId = sc.SubCategoryId,
                    Name = sc.Name , 
                    CategoryId = sc.CategoryId , 
                    CategoryName = c.Name,
                }).ToList()
            }); 
        }
        public async Task<CategoryDTO> GetByIDAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} was not found");
            }

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                SubCategories = category.SubCategories.Select(sc => new SubCategoryDTO
                {
                    SubCategoryId = sc.SubCategoryId,
                    Name = sc.Name,
                    CategoryId = sc.CategoryId,
                    CategoryName = category.Name
                }).ToList()
            };
        }
        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                SubCategories = new List<SubCategoryDTO>()
            };
        }
     
        public async Task<CategoryDTO> UpdateAsync(int id, UpdateCategoryDTO dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with id {id} was not found.");

            category.Name = dto.Name;

            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                SubCategories = category.SubCategories.Select(sc => new SubCategoryDTO
                {
                    SubCategoryId = sc.SubCategoryId,
                    Name = sc.Name,
                    CategoryId = sc.CategoryId,
                    CategoryName = category.Name
                }).ToList()
            };
        }
       
        public async Task<CategoryDTO> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with id {id} was not found.");

            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                SubCategories = category.SubCategories.Select(sc => new SubCategoryDTO
                {
                    SubCategoryId = sc.SubCategoryId,
                    Name = sc.Name,
                    CategoryId = sc.CategoryId,
                    CategoryName = category.Name
                }).ToList()
            };
        }



    }
}
