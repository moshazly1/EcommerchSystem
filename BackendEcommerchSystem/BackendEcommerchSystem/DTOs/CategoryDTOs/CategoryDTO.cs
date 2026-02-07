using BackendEcommerchSystem.DTOs.CategoryDTOs;
using System.ComponentModel.DataAnnotations;

namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }
        public string? ParentName { get; set; }  

        public List<SubCategory>? SubCategories { get; set; }  
    }
}
