namespace OrderManagementAPI.Models
{
    public class Order
    {
        public string Id { get; set; } 
        public DateTime OrderDate { get; set; } 
        public string CustomerId { get; set; }
    }
}
