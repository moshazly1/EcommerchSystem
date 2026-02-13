using BackendEcommerchSystem.DTOs.ProductDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetAllProduct();
        Task<ProductDetailsDTO> GetProductByID(int id);
        Task<ProductDetailsDTO> AddProduct(CreateProductDTO dto);
        Task<ProductDetailsDTO> UpdateProduct(int id, UpdateProductDTO dto);
        Task DeleteProduct(int id);

    }     

}
