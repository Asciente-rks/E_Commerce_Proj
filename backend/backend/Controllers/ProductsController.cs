using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products = new List<Product>();

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            newProduct.ID = Guid.NewGuid();
            Products.Add(newProduct);
            return CreatedAtAction(nameof(GetByID), new { id = newProduct.ID }, newProduct);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var product = Products.Find(p => p.ID == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Product updatedProduct)
        {
            var product = Products.Find(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = Products.Find(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            Products.Remove(product);
            return Ok(product);
        }
    }
}
