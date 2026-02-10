using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        // علاقة مع المنتجات
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
