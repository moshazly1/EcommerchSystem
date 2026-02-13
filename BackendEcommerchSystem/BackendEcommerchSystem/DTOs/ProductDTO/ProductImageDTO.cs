using BackendEcommerchSystem.Enums;

namespace BackendEcommerchSystem.DTOs.ProductDTO
{
    public class ProductImageDTO
    {
        public string ImageUrl { get; set; } = string.Empty;
        public ProductImageType ImageType { get; set; }
    }
}
