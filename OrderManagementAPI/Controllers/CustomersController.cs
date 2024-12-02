using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DataBase;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DataStore _dataStore;

        public CustomersController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        // Create - Dodavanje kupca
        [HttpPost]
        public IActionResult CreateCustomer(CustomerCreateRequest request)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Email = request.Email
            };
            _dataStore.Customers.Add(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // Read - Dohvaćanje svih kupaca
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_dataStore.Customers);
        }

        // Read - Dohvaćanje pojedinačnog kupca
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(string id)
        {
            var customer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // Update - Ažuriranje kupca
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, CustomerUpdateRequest request)
        {
            var customer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            customer.Name = request.Name;
            customer.Email = request.Email;

            return NoContent();
        }

        // Delete - Brisanje kupca
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            if (_dataStore.Orders.Any(o => o.CustomerId == id))
                return BadRequest("Cannot delete customer with existing orders.");

            _dataStore.Customers.Remove(customer);
            return NoContent();
        }
    }


}
