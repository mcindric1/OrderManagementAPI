namespace OrderManagementAPI.DTO
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}
