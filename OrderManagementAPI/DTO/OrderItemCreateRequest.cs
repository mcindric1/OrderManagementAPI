namespace OrderManagementAPI.DTO
{
    public class OrderItemCreateRequest
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
