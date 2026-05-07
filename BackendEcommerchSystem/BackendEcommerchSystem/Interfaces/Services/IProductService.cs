using BackendEcommerchSystem.DTOs.ProductDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetAllProduct();
        Task<PagedResaltDTO<ProductListDTO>> GetAllLaptop(int page, int pageSize);
        Task<PagedResaltDTO<ProductListDTO>> GetAllPCs(int page, int pageSize);
        Task<PagedResaltDTO<ProductListDTO>> GetAllMice(int page  , int pageSize );
        Task<PagedResaltDTO<ProductListDTO>> GetAllAccessories(int page , int pageSize);
        Task<ProductDetailsDTO> GetProductByID(int id);
     
        Task<ProductDetailsDTO> AddProduct(CreateProductDTO dto);
        Task<ProductDetailsDTO> UpdateProduct(int id, UpdateProductDTO dto);
        Task DeleteProduct(int id);

    }     

}
