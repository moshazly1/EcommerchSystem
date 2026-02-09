using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class CategoryDTO
    {

        
            public int CategoryId { get; set; }
            public string Name { get; set; }

            // لو حابب ترجع كل SubCategories مع كل Category:
            public ICollection<SubCategoryDTO>? SubCategories { get; set; }
    

    }
}
