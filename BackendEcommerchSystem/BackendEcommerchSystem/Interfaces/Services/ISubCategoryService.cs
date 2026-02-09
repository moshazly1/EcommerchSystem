using BackendEcommerchSystem.DTOs.SubCategoryDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategoryDTO>> GetAllAsync();

        Task<SubCategoryDTO> GetByIdAsync(int id);

        Task<SubCategoryDTO> CreateAsync(SubCreateDTO dto);

        Task<SubCategoryDTO> UpdateAsync(int id, UpdateSubCategoryDTO dto);

        Task<SubCategoryDTO> DeleteAsync(int id);
    }
}
