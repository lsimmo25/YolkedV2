using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YolkedWorkoutLogger.Server.Models;

namespace YolkedWorkoutLogger.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foods = await _context.Foods
                .Where(f => f.UserId == userId)
                .ToListAsync();
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood([FromBody] Food food)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            food.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, food);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, [FromBody] Food food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (food.UserId != userId)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(food).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Foods.Any(f => f.Id == id && f.UserId == userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
            if (food == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
