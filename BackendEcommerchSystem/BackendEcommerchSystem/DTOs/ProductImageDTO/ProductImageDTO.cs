using BackendEcommerchSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.ProductImageDTO
{
    public class ProductImageDTO
    {

        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageType { get; set; } = string.Empty;
    }
}
