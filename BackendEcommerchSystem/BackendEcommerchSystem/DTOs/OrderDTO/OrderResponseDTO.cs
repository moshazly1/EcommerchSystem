namespace BackendEcommerchSystem.DTOs.OrderDTO
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        public List<OrderItemsRespnseDTO> Items { get; set; }
    }
}
