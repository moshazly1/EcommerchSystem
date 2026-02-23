using BackendEcommerchSystem.DTOs.OrderDTO;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{

    public class OrderServices : IOrderServices
    {

        public readonly IOrderServices _orderServise;

        public OrderServices (IOrderServices orderServices)
        {
            _orderServise = orderServices;      
        }
        public Task CreateOrderAsync(CreateOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponseDTO> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderListDTO>> GetOrdersByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatus(UpdateOrderStatusDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
