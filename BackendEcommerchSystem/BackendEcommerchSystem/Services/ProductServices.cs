using BackendEcommerchSystem.DTOs.ProductDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository) {

            _productRepository = productRepository;       
        }        

        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            var product =  await _productRepository.GetAllProductsAsync();

            var resalt = product.Select(p => new ProductDTO
            {
                Id = p.Id,  
                Name = p.Name,  
                 Description = p.Description,   
                 Stock = p.Stock,   
                 Price= p.Price,  
                 Condition= p.Condition,    
                 IsActive= p.IsActive,  
                 BrandName=p.Brand?.Name ?? "",
                 SubCategoryName = p.SubCategory?.Name ??  "" , 
                 CreatedAt= p.CreatedAt, 
                 

            }); 
            return resalt; 
        }

        public async Task<ProductDTO> GetProductByID(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id); 
            if(product == null )
            {
                throw new Exception("Product not found");
            }
            var resalt = new ProductDTO
            {
                Id = product.Id,
                BrandName = product.Brand?.Name ?? string.Empty,
                Condition = product.Condition,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                SubCategoryName = product.SubCategory?.Name ?? string.Empty , 
                Name = product.Name,
            }; 
            return resalt;
        }

        public async Task<ProductDTO> AddProduct(CreateProductDTO dto)
        {
            var product = new Product
            {
                Stock = dto.Stock,
                BrandId = dto.BrandId,
                Name=dto.Name,
                Condition = dto.Condition,      
                Description = dto.Description,      
                Price = dto.Price,
                IsActive = true,
                CreatedAt = DateTime.Now,       
            };
            await _productRepository.AddPrductAsync(product);
           

            return new ProductDTO
            {
                Id = product.Id,        
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Condition = product.Condition,
                IsActive = product.IsActive

            }; 



        }

        public async Task DeleteProduct(int id)
        {
               await _productRepository.DeleteProduct(id); 
        }

      

        public async Task<ProductDTO> UpdateProduct(int id, UpdateProductDTO dto)
        {
            var product = await _productRepository.GetByIdProductAsync(id);
            if (product == null )
            {
                throw new Exception("Product not found");
            }
            product.Name = dto.Name;
            product.Description = dto.Description;  
            product.Price = dto.Price;  
            product.Stock = dto.Stock;  
            product.Condition = dto.Condition;  
            product.IsActive = dto.IsActive;    
      

            await _productRepository.UpdateProduct(id , product);


            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Condition = product.Condition,
                IsActive = product.IsActive

            };

        }
    }
}
