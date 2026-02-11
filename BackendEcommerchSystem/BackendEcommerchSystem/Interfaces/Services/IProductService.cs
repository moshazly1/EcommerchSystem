using BackendEcommerchSystem.DTOs.ProductDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProduct();
        Task<ProductDTO> GetProductByID(int id);
        Task<ProductDTO> AddProduct(CreateProductDTO dto);
        Task<ProductDTO> UpdateProduct(int id, UpdateProductDTO dto);
        Task DeleteProduct(int id);

    }     

}
