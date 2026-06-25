using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appContext;

        public CartRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _appContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<CartItem?> GetCartItemAsync(int cartId, int productId)
        {
            return await _appContext.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _appContext.Carts.AddAsync(cart);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _appContext.CartItems.AddAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _appContext.CartItems.Update(cartItem);
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            _appContext.CartItems.Remove(cartItem);
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}