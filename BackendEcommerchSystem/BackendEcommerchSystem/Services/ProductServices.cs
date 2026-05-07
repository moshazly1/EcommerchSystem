using BackendEcommerchSystem.DTOs.ProductDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Enums;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using Org.BouncyCastle.Utilities;

namespace BackendEcommerchSystem.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductServices(IProductRepository productRepository) {

            _productRepository = productRepository;       
        }        

        public async Task<IEnumerable<ProductListDTO>> GetAllProduct()
        {
            var product =  await _productRepository.GetAllProductsAsync();

            var resalt = product.Select(p => new ProductListDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Condition = p.Condition,
                MainImageUrl = p.ProductImages
                 .FirstOrDefault(img => img.ImageType == ProductImageType.Main)?.ImageUrl
                 ?? p.ProductImages.FirstOrDefault()?.ImageUrl
                 ?? ""
            });
            return resalt; 
        }

        public async Task<ProductDetailsDTO> GetProductByID(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id); 
            if(product == null )
            {
                throw new Exception("Product not found");
            }
            var resalt = new ProductDetailsDTO
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
                Images = product.ProductImages.Select(img => new ProductImageDTO
                {
                    ImageType = img.ImageType,  
                    ImageUrl = img.ImageUrl,        
                }).ToList()
            }; 
            return resalt;
        }

        public async Task<ProductDetailsDTO> AddProduct(CreateProductDTO dto)
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
           

            return new ProductDetailsDTO
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

      

        public async Task<ProductDetailsDTO> UpdateProduct(int id, UpdateProductDTO dto)
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


            return new ProductDetailsDTO
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

        public async Task<PagedResaltDTO<ProductListDTO>> GetAllLaptop(int page, int pageSize)
        {
            var (products, totalCount) = await _productRepository.GetAllLaptopAsync(page, pageSize);

            var items = products.Select(p => new ProductListDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Condition = p.Condition,
                MainImageUrl = p.ProductImages
                 .FirstOrDefault(img => img.ImageType == ProductImageType.Main)?.ImageUrl
                 ?? p.ProductImages.FirstOrDefault()?.ImageUrl
                 ?? ""
            });

            return new PagedResaltDTO<ProductListDTO>
            {
          Items = items, 
          Page = page,
          TotalCount = totalCount,
          PageSize = pageSize   , 
          TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize)
    
            };
        
        }

        public  async Task<PagedResaltDTO<ProductListDTO>> GetAllPCs(int page, int pageSize)
        {
            var (product , totalCount) =  await _productRepository.GetAllPCsAsync(page , pageSize);

            var items = product.Select(p => new ProductListDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Condition = p.Condition,
                MainImageUrl = p.ProductImages
                 .FirstOrDefault(img => img.ImageType == ProductImageType.Main)?.ImageUrl
                 ?? p.ProductImages.FirstOrDefault()?.ImageUrl
                 ?? ""
            });
            return new PagedResaltDTO<ProductListDTO>
            {
                Items = items,           
                TotalCount = totalCount, 
                Page = page,            
                PageSize = pageSize,    
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) // 21
            };
        }

        public async Task<PagedResaltDTO<ProductListDTO>> GetAllMice(int page, int pageSize)
        {
            var (product , totalCount) = await _productRepository.GetAllMiceAsync(page , pageSize);

            var items = product.Select(p => new ProductListDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Condition = p.Condition,
                MainImageUrl = p.ProductImages
                 .FirstOrDefault(img => img.ImageType == ProductImageType.Main)?.ImageUrl
                 ?? p.ProductImages.FirstOrDefault()?.ImageUrl
                 ?? ""
            });
            return new PagedResaltDTO<ProductListDTO>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) // 21
            };
        }

        public async Task<PagedResaltDTO<ProductListDTO>> GetAllAccessories(int page, int pageSize)
        {
            var (product , totalCount) = await _productRepository.GetAllAccessoriesAsync(page , pageSize);

            var items = product.Select(p => new ProductListDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Condition = p.Condition,
                MainImageUrl = p.ProductImages
                 .FirstOrDefault(img => img.ImageType == ProductImageType.Main)?.ImageUrl
                 ?? p.ProductImages.FirstOrDefault()?.ImageUrl
                 ?? ""
            });
            return new PagedResaltDTO<ProductListDTO>
            {

                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) // 21
            }; 
        }
    }
}
