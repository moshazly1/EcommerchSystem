using BackendEcommerchSystem.DTOs.CartDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartResponseDTO> GetCartAsync(int userId);
        Task<CartResponseDTO> AddToCartAsync(int userId, AddToCartDto dto);
        Task<CartResponseDTO> UpdateQuantityAsync(int userId, int cartItemId, int quantity);
        Task RemoveItemAsync(int userId, int cartItemId);
    }
}
