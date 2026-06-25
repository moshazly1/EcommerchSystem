using BackendEcommerchSystem.DTOs.CartDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartResponseDTO> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return new CartResponseDTO();
            return MapToCartResponseDTO(cart);
        }

        public async Task<CartResponseDTO> AddToCartAsync(int userId, AddToCartDto dto)
        {
           
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                
                cart = new Cart { UserId = userId };
                await _cartRepository.AddCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

          
            var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, dto.ProductId);
            if (existingItem != null)
            {
                // 4. زود الكمية
                existingItem.Quantity += dto.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
              
                var product = await _productRepository.GetByIdProductAsync(dto.ProductId);
                if (product == null) throw new Exception("Product not found");

              
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price
                };
                await _cartRepository.AddCartItemAsync(cartItem);
            }

            await _cartRepository.SaveChangesAsync();

        
            var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
            return MapToCartResponseDTO(updatedCart!);
        }

        public async Task<CartResponseDTO> UpdateQuantityAsync(int userId, int cartItemId, int quantity)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) throw new Exception("Cart not found");

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item == null) throw new Exception("Item not found");

            item.Quantity = quantity;
            await _cartRepository.UpdateCartItemAsync(item);
            await _cartRepository.SaveChangesAsync();

            var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
            return MapToCartResponseDTO(updatedCart!);
        }

        public async Task RemoveItemAsync(int userId, int cartItemId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) throw new Exception("Cart not found");

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item == null) throw new Exception("Item not found");

            await _cartRepository.RemoveCartItemAsync(item);
            await _cartRepository.SaveChangesAsync();
        }

     
        private CartResponseDTO MapToCartResponseDTO(Cart cart)
        {
            var items = cart.CartItems.Select(ci => new CartItemResponseDTO
            {
                CartItemId = ci.Id,
                ProductId = ci.ProductId,

                ProductName = ci.Product?.Name ?? string.Empty,
                ProductDescription = ci.Product?.Description ?? string.Empty,
                ImageUrl = ci.Product?.ProductImages?.FirstOrDefault()?.ImageUrl,
                UnitPrice = ci.UnitPrice,
                Quantity = ci.Quantity
            }).ToList();

            var subtotal = items.Sum(i => i.TotalPrice);
            var shipping = 25m;
            var tax = subtotal * 0.08m;

            return new CartResponseDTO
            {
                CartId = cart.Id,
                Items = items,
                Subtotal = subtotal,
                Shipping = shipping,
                Tax = Math.Round(tax, 2),
                Total = Math.Round(subtotal + shipping + tax, 2)
            };
        }
    }
}