namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class CreateCategoryDTO
    {
    
        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }
    }
}
