namespace BackendEcommerchSystem.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {
     

        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }
}
