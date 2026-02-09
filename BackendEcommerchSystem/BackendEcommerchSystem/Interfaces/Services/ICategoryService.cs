using BackendEcommerchSystem.DTOs.CategoryDTO;
using BackendEcommerchSystem.Entities;
namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface ICategoryService
    {
       Task<IEnumerable<CategoryDTO>> GetAllAsync();
       Task<CategoryDTO> GetByIDAsync(int id);
        Task<CategoryDTO> CreateAsync(CreateCategoryDTO categoryDTO);

        Task<CategoryDTO> UpdateAsync(int id , UpdateCategoryDTO dto); 
      Task<CategoryDTO> DeleteAsync(int id);
        
    }
}
