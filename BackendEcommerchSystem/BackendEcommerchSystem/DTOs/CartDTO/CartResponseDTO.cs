namespace BackendEcommerchSystem.DTOs.CartDTO
{
    public class CartResponseDTO
    {
        public int CartId { get; set; }
        public List<CartItemResponseDTO> Items { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}