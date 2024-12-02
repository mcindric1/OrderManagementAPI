using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DataBase;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly DataStore _dataStore;

        public OrderItemsController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        // Create - Dodavanje stavke narudžbe
        [HttpPost]
        public IActionResult CreateOrderItem(OrderItemCreateRequest request)
        {
            if (!_dataStore.Orders.Any(o => o.Id == request.OrderId))
                return BadRequest("Order does not exist.");
            if (!_dataStore.Products.Any(p => p.Id == request.ProductId))
                return BadRequest("Product does not exist.");

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            _dataStore.OrderItems.Add(orderItem);

            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
        }

        // Read - Dohvaćanje svih stavki narudžbi
        [HttpGet]
        public IActionResult GetAllOrderItems()
        {
            return Ok(_dataStore.OrderItems);
        }

        // Read - Dohvaćanje pojedinačne stavke narudžbe
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(string id)
        {
            var orderItem = _dataStore.OrderItems.FirstOrDefault(oi => oi.Id == id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        // Update - Ažuriranje stavke narudžbe
        [HttpPut("{id}")]
        public IActionResult UpdateOrderItem(string id, OrderItemUpdateRequest request)
        {
            var orderItem = _dataStore.OrderItems.FirstOrDefault(oi => oi.Id == id);
            if (orderItem == null) return NotFound();

            orderItem.Quantity = request.Quantity;

            return NoContent();
        }

        // Delete - Brisanje stavke narudžbe
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(string id)
        {
            var orderItem = _dataStore.OrderItems.FirstOrDefault(oi => oi.Id == id);
            if (orderItem == null) return NotFound();

            _dataStore.OrderItems.Remove(orderItem);
            return NoContent();
        }
    }
}
