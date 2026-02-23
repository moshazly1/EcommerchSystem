using BackendEcommerchSystem.DTOs.OrderDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IOrderServices
    {
        Task CreateOrderAsync(CreateOrderDTO dto);

        Task<IEnumerable<OrderListDTO>> GetOrdersByUserId(int userId);

        Task<OrderResponseDTO> GetOrderById(int orderId);

        Task UpdateOrderStatus(UpdateOrderStatusDTO dto);
    }
}
