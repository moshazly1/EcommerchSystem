namespace BackendEcommerchSystem.DTOs.OrderDTO
{
    public class CreateOrderItemsDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
