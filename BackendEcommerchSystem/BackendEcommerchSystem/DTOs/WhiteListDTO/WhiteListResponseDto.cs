namespace BackendEcommerchSystem.DTOs.WhiteListDTO
{
    public class WhiteListResponseDto
    {
        public int Id { get; set; }     
        public int ProductId { get; set; }    
        public int stock {  get; set; }     
        public string name { get; set; } = string.Empty;
       public string description { get; set; }  
        public string? mainImageUrl { get; set; }       

        public decimal Price { get; set; }      
    }
}
