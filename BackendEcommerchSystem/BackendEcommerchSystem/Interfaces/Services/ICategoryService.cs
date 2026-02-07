using BackendEcommerchSystem.DTOs.CategoryDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAllAsync(); 
        public Task<CategoryDTO> GetByIDAsync(int id);
        public Task<CategoryDTO> CreateAsync(CreateCategoryDTO categoryDTO);

        public Task<CategoryDTO> UpdateAsync(int id , UpdateCategoryDTO dto); 
        public Task<CategoryDTO> DeleteAsync(int id);  
        
    }
}
