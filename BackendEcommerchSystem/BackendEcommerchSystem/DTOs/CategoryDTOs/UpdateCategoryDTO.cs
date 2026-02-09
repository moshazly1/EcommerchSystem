using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {


    
            [Required]
            [MaxLength(150)]
            public string Name { get; set; } = string.Empty;
      

    }
}
