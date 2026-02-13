using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.BrandDTO
{
    public class BrandDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
