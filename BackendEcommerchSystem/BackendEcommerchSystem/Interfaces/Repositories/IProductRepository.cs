using BackendEcommerchSystem.Entities;
namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllLaptopAsync(int page, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllPCsAsync(int page, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllMiceAsync(int page, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAccessoriesAsync(int page, int pageSize);
        Task<Product> GetByIdProductAsync(int id);
        Task AddPrductAsync(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
        Task SaveChangesAsync();
    }
}