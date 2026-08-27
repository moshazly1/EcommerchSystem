namespace BackendEcommerchSystem.DTOs.FiltrationDTO
{
    public class ProductFilterDTO
    {
        public int?  MinPrice { get; set; }  
        public int? MaxPrice { get; set; }  
        public int?  CategoryId { get; set; }    

        public int? SubCategoryId { get; set; } 

        public int? BrandID { get; set; }
        
    }
}
