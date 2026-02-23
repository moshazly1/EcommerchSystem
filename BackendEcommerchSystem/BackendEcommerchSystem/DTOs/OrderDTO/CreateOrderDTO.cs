namespace BackendEcommerchSystem.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }     
        public List<CreateOrderItemDTO> items { get; set; } 

        public string ShippingAddress { get; set; }     

        public decimal TotalAmount { get; set; }        
    }
}
