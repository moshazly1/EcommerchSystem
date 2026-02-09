namespace BackendEcommerchSystem.DTOs.SubCategoryDTO
{
    public class SubCategoryDTO
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
