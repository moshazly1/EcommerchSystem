using BackendEcommerchSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.ProductDTO
{
    public class UpdateProductDTO
    {

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public ProductCondition Condition { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
