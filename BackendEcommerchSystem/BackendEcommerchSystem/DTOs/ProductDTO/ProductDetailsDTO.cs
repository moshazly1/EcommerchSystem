using BackendEcommerchSystem.Enums;

namespace BackendEcommerchSystem.DTOs.ProductDTO
{
    public class ProductDetailsDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public ProductCondition Condition { get; set; }

        public bool IsActive { get; set; }

        public string BrandName { get; set; } = string.Empty;

        public string SubCategoryName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public List<ProductImageDTO> Images { get; set; } = new();
    }
}
