using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Enums;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;  
        public OrderRepository(AppDbContext context) { 
        _context = context;     
        }        
        public async Task AddOrder(Order order)
        {
          await  _context.orders.AddAsync(order);   
            await _context.SaveChangesAsync();          
        }
        public async Task DeleteOrder(int orderID)
        {
            var order = await _context.orders.FirstOrDefaultAsync(x=>x.Id == orderID); 
            if(order != null)
            {
                 _context.orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        } 
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _context.orders.ToListAsync();
            return orders;      
        }
        public async Task<Order> GetOrder(int orderID)
        {
            var order = await  _context.orders.FirstOrDefaultAsync(x=>x.Id == orderID);
        
            return order;
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            var Order  = await  _context.orders.Where(x=>x.CustomerId == customerId).ToListAsync();
            return Order; 
        }
        public  async Task UpdateOrder(Order order)
        {
             _context.orders.Update(order); 
              await  _context.SaveChangesAsync();       
        }
        public async Task UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var  order = await _context.orders.FirstOrDefaultAsync(x=>x.Id == orderId);
            if(order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
