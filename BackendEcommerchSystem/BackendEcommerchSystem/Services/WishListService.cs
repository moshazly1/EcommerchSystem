using BackendEcommerchSystem.DTOs.WhiteListDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class WhiteListService : IWhiteListServices
    {
        private readonly IWhiteListReposatory _whiteListReposatory;

        public WhiteListService(IWhiteListReposatory whiteListReposatory)
        {
            _whiteListReposatory = whiteListReposatory;
        }

        public async Task AddAsync(int userId, AddWhiteListDTO dto)
        {
            var existing = await _whiteListReposatory
                .GetByUserAndProductAsync(userId, dto.ProductId);

            if (existing != null)
            {
                throw new Exception("Product already exists in wishlist.");
            }

            var whiteList = new WhiteList
            {
                UserId = userId,
                ProductId = dto.ProductId,
                CreatedAt = DateTime.UtcNow
            };

            await _whiteListReposatory.AddWhiteListAsync(whiteList);
            await _whiteListReposatory.SaveChangesAsync();
        }

        public async Task<IEnumerable<WhiteListResponseDto>> GetWhiteListaysnc(int userId)
        {
            var whiteLists = await _whiteListReposatory.GetWhiteListsByUserID(userId);

            if (!whiteLists.Any())
            {
                throw new Exception("Wishlist is empty.");
            }

            return whiteLists.Select(item => new WhiteListResponseDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                name = item.Product!.Name,
                description = item.Product!.Description,    
                Price = item.Product.Price,
                stock = item.Product.Stock,     
                mainImageUrl = item.Product.ProductImages
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()
            });
        }

        public async Task RemoveAsync(int userId, AddWhiteListDTO dto)
        {
            var whiteList = await _whiteListReposatory
                .GetByUserAndProductAsync(userId, dto.ProductId);

            if (whiteList == null)
            {
                throw new Exception("Product not found in wishlist.");
            }

            await _whiteListReposatory.RemoveAsync(whiteList);
            await _whiteListReposatory.SaveChangesAsync();
        }
    }
}