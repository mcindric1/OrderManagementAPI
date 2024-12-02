using OrderManagementAPI.Models;

namespace OrderManagementAPI.DataBase
{
    public class DataStore
    {
        public List<Customer> Customers { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<OrderItem> OrderItems { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }

}
