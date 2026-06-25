namespace BackendEcommerchSystem.DTOs.CartDTO
{
    public class CartItemResponseDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; }  
        public string? ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
