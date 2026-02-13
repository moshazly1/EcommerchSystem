using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IProductImageRepository
    {

        Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
        Task<ProductImage?> GetByIdAsync(int id);
        Task AddAsync(ProductImage image);
        Task UpdateAsync(ProductImage image);
        Task DeleteAsync(ProductImage image);

    }
}
