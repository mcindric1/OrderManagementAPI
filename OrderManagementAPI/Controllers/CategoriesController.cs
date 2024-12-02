using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.DataBase;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataStore _dataStore;

        public CategoriesController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        // Create - Dodavanje kategorije
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name
            };
            _dataStore.Categories.Add(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // Read - Dohvaćanje svih kategorija
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_dataStore.Categories);
        }

        // Read - Dohvaćanje pojedinačne kategorije
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(string id)
        {
            var category = _dataStore.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // Update - Ažuriranje kategorije
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(string id, CategoryUpdateRequest request)
        {
            var category = _dataStore.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            category.Name = request.Name;

            return NoContent();
        }

        // Delete - Brisanje kategorije
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(string id)
        {
            var category = _dataStore.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            if (_dataStore.Products.Any(p => p.CategoryId == id))
                return BadRequest("Cannot delete category with associated products.");

            _dataStore.Categories.Remove(category);
            return NoContent();
        }
    }


}
