using BackendEcommerchSystem.DTOs.OrderDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _OrderReposatory;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public OrderServices(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IProductRepository productRepository)
        {
            _OrderReposatory = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO dto)
        {
            // 1. جيب الـ Cart
            var cart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);
            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty");

            // 2. اعمل Order
            var order = new Order
            {
                CustomerId = dto.UserId,
                TotalPrice = dto.TotalAmount,
                CreatedAt = DateTime.UtcNow,
                Status = Enums.OrderStatus.Pending
            };

            // 3. حول CartItems لـ OrderItems
            order.OrderItems = cart.CartItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.UnitPrice
            }).ToList();

            // 4. قلل الـ Stock لكل منتج
            foreach (var item in cart.CartItems)
            {
                var product = await _productRepository.GetByIdProductAsync(item.ProductId);
                if (product == null) throw new Exception($"Product {item.ProductId} not found");
                if (product.Stock < item.Quantity) throw new Exception($"Not enough stock for {product.Name}");
                product.Stock -= item.Quantity;
                await _productRepository.UpdateProduct(product.Id, product);
            }

            // 5. احفظ الـ Order
            await _OrderReposatory.AddOrder(order);

            // 6. امسح الـ Cart
            foreach (var item in cart.CartItems.ToList())
            {
                await _cartRepository.RemoveCartItemAsync(item);
            }
            await _cartRepository.SaveChangesAsync();
        }

        // باقي الـ methods زي ما هي
        public async Task<OrderResponseDTO> GetOrderById(int orderId)
        {
            var order = await _OrderReposatory.GetOrder(orderId);
            if (order == null) throw new Exception("Order not found");
            return new OrderResponseDTO
            {
                OrderId = order.Id,
                OrderDate = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalPrice,
            };
        }

        public async Task<IEnumerable<OrderListDTO>> GetOrdersByUserId(int userId)
        {
            var orders = await _OrderReposatory.GetOrdersByCustomerId(userId);
            return orders.Select(o => new OrderListDTO
            {
                OrderId = o.Id,
                OrderDate = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalPrice
            });
        }

        public async Task UpdateOrderStatus(UpdateOrderStatusDTO dto)
        {
            await _OrderReposatory.UpdateOrderStatus(dto.OrderId, dto.Status);
        }
    }
}