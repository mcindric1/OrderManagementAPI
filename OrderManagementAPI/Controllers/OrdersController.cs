using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DataBase;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly DataStore _dataStore;

        public OrdersController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        // Create - Dodavanje narudžbe
        [HttpPost]
        public IActionResult CreateOrder(OrderCreateRequest request)
        {
            if (!_dataStore.Customers.Any(c => c.Id == request.CustomerId))
                return BadRequest("Customer does not exist.");

            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                OrderDate = DateTime.UtcNow,
                CustomerId = request.CustomerId
            };
            _dataStore.Orders.Add(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // Read - Dohvaćanje svih narudžbi
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_dataStore.Orders);
        }

        // Read - Dohvaćanje pojedinačne narudžbe
        [HttpGet("{id}")]
        public IActionResult GetOrderById(string id)
        {
            var order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // Update - Ažuriranje narudžbe
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, OrderUpdateRequest request)
        {
            var order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            order.OrderDate = request.OrderDate;

            return NoContent();
        }

        // Delete - Brisanje narudžbe
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(string id)
        {
            var order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            if (_dataStore.OrderItems.Any(oi => oi.OrderId == id))
                return BadRequest("Cannot delete order with existing order items.");

            _dataStore.Orders.Remove(order);
            return NoContent();
        }
    }


}
