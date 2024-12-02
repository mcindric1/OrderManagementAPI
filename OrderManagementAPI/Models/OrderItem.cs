namespace OrderManagementAPI.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
