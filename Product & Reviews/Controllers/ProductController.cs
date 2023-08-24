using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product___Reviews.Data;
using Product___Reviews.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product___Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var Products = _context.Products.ToList();
            return Ok(Products);
        }

        

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var products = _context.Products.Where(p => p.Id == id);
            
            if(products == null) { return NotFound(); }
            return Ok(products);
        }

        [HttpGet("api/{id}")]
        public IActionResult GetProduct(int id)
        {
            var productDTO = _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.Reviews) 
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Reviews = p.Reviews,
                    AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0
                })
               .ToList();

            if (productDTO == null)
            {
                return NotFound();
            }

            return Ok(productDTO);
        }
        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("api/products")]
        public IActionResult GetProducts([FromQuery] decimal? maxPrice = null)
        {
            IQueryable<Product> query = _context.Products;

            if (maxPrice.HasValue)
            {
                query = query.Where(p => (decimal)p.Price <= maxPrice);
            }

            var products = query.ToListAsync();
            return Ok(products);
        }
    }
}
