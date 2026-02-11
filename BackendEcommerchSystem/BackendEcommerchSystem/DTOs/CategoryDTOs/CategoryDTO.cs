using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class CategoryDTO
    {        
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public ICollection<BackendEcommerchSystem.DTOs.SubCategoryDTO.SubCategoryDTO>? SubCategories { get; set; }
    }
}
