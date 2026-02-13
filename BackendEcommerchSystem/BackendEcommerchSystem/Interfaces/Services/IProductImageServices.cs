using BackendEcommerchSystem.DTOs.ProductImageDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IProductImageServices
    {
        Task<IEnumerable<ProductImageDTO>> GetImagesByProductIdAsync(int productId);
        Task<ProductImageDTO> AddImageAsync(CreateProductImageDTO dto);
        Task DeleteImageAsync(int imageId);
    }
}
