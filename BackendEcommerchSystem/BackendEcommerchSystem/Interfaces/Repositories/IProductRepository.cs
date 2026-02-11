using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetByIdProductAsync(int id);
        Task AddPrductAsync(Product product);
        Task UpdateProduct(int id ,  Product product);   
        Task DeleteProduct(int id);
        Task SaveChangesAsync(); 

    }
}
