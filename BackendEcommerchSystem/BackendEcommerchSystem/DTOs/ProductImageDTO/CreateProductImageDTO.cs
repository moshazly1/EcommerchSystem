using BackendEcommerchSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.ProductImageDTO
{
    public class CreateProductImageDTO
    {

        public int ProductId { get; set; }

    
        public string ImageUrl { get; set; } = string.Empty;

    
        public ProductImageType ImageType { get; set; }
    }
}
