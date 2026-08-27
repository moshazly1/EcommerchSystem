using BackendEcommerchSystem.DTOs.FiltrationDTO;
using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IProductFilterReposatory
    {
        Task<List<Product>> GetFilteredProductsAsync(ProductFilterDTO dto);
    }
}
