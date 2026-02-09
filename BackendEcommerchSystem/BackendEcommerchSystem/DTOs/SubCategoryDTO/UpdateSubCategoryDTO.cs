using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.SubCategoryDTO
{
    public class UpdateSubCategoryDTO
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
