using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllLaptopAsync();
        Task<IEnumerable<Product>> GetAllPCsAsync();
        Task<IEnumerable<Product>> GetAllMiceAsync();
        Task<IEnumerable<Product>> GetAllAccessoriesAsync();
        Task<Product> GetByIdProductAsync(int id);
        Task AddPrductAsync(Product product);
        Task UpdateProduct(int id ,  Product product);   
        Task DeleteProduct(int id);
        Task SaveChangesAsync();  

       

    }
}
