using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class ProductImageReposatory : IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageReposatory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            return await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ProductImage image)
        {
            await _context.ProductImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductImage image)
        {
            _context.ProductImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductImage image)
        {
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
