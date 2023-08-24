using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product___Reviews.Data;
using Product___Reviews.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product___Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ReviewController>
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews.ToList();
            return Ok(reviews);
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reviews = _context.Reviews.Where(r => r.Id == id);

            if (reviews == null) { return NotFound(); }

            return Ok(reviews);
        }

        // POST api/<ReviewController>
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return StatusCode(201, review);
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review reviewData)
        {
            var existingReview = _context.Reviews.Find(id);
            if(existingReview == null)
            {
                return NotFound();
            }

            existingReview.Rating = reviewData.Rating;
            existingReview.Text = reviewData.Text;
            _context.SaveChanges();
            return Ok(existingReview);
        }

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Reviews = _context.Reviews.Find(id);
            if(Reviews == null)
            {
                NotFound();
            }
            _context.Reviews.Remove(Reviews);
            return NoContent();
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetByProductId(int productId)
        {
            var reviews = _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToList();

            return Ok(reviews);
        }
    }
}
