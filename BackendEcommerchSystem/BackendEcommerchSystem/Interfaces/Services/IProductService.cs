using BackendEcommerchSystem.DTOs.ProductDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetAllProduct();
        Task<IEnumerable<ProductListDTO>> GetAllLaptop();
        Task<IEnumerable<ProductListDTO>> GetAllPCs();
        Task<IEnumerable<ProductListDTO>> GetAllMice();
        Task<IEnumerable<ProductListDTO>> GetAllAccessories();
        Task<ProductDetailsDTO> GetProductByID(int id);
     
        Task<ProductDetailsDTO> AddProduct(CreateProductDTO dto);
        Task<ProductDetailsDTO> UpdateProduct(int id, UpdateProductDTO dto);
        Task DeleteProduct(int id);

    }     

}
