using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
   

        public class CreateCategoryDTO
        {
            [Required]
            [MaxLength(150)]
            public string Name { get; set; } = string.Empty;
        }

    
}
