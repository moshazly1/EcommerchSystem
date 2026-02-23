using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Enums;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
        Task UpdateOrderStatus(int orderId, OrderStatus  status);

        Task<Order> GetOrder(int  orderID);
        Task AddOrder(Order order);     
        Task DeleteOrder(int orderID);  
        Task UpdateOrder(Order order);


    }
}
