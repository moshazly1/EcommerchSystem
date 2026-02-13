using BackendEcommerchSystem.DTOs.ProductImageDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Enums;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;

namespace BackendEcommerchSystem.Services
{
    public class ProductImageService : IProductImageServices
    {
        private readonly IProductImageRepository _imageRepo;
        private readonly IProductRepository _productRepo;
        public ProductImageService(IProductImageRepository imgeRepo, IProductRepository productRepo) { 
          _imageRepo = imgeRepo;   
            _productRepo=productRepo;   
       }

        public async Task<ProductImageDTO> AddImageAsync(CreateProductImageDTO dto)
        {
        
            var product = await _productRepo.GetByIdProductAsync(dto.ProductId);
            if (product == null)
                throw new Exception("Product not found");

      
            if (dto.ImageType == ProductImageType.Main)
            {
                var images = await _imageRepo.GetByProductIdAsync(dto.ProductId);
                foreach (var img in images.Where(x => x.ImageType == ProductImageType.Main))
                {
                    img.ImageType = ProductImageType.Front; 
                    await _imageRepo.UpdateAsync(img);
                }
            }
            var image = new ProductImage
            {
                ProductId = dto.ProductId,
                ImageUrl = dto.ImageUrl,
                ImageType = dto.ImageType
            };

            await _imageRepo.AddAsync(image);

            return new ProductImageDTO
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                ImageType = image.ImageType.ToString()
            };
        }

        public async Task DeleteImageAsync(int imageId)
        {

            var image = await _imageRepo.GetByIdAsync(imageId);
            if (image == null)
            {
                throw new Exception("Image not found");

            }

            await _imageRepo.DeleteAsync(image);
        }

        public async Task<IEnumerable<ProductImageDTO>> GetImagesByProductIdAsync(int productId)
        {
            var images = await _imageRepo.GetByProductIdAsync(productId);
            if (images == null || !images.Any())
            {
                throw new Exception("No images found for this product");
            }

            return images.Select(i => new ProductImageDTO
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                ImageType = i.ImageType.ToString()
            });
        }
    
    }
}
