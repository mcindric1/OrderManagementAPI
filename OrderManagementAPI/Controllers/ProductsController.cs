using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DataBase;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataStore _dataStore;

        public ProductsController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        // Create - Dodavanje proizvoda
        [HttpPost]
        public IActionResult CreateProduct(ProductCreateRequest request)
        {
            if (!_dataStore.Categories.Any(c => c.Id == request.CategoryId))
                return BadRequest("Category does not exist.");

            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            _dataStore.Products.Add(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // Read - Dohvaćanje svih proizvoda
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_dataStore.Products);
        }

        // Read - Dohvaćanje pojedinačnog proizvoda
        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            var product = _dataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // Update - Ažuriranje proizvoda
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id, ProductUpdateRequest request)
        {
            var product = _dataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            return NoContent();
        }

        // Delete - Brisanje proizvoda
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var product = _dataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            if (_dataStore.OrderItems.Any(oi => oi.ProductId == id))
                return BadRequest("Cannot delete product used in an order.");

            _dataStore.Products.Remove(product);
            return NoContent();
        }
    }


}
