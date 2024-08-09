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
    public class BodyWeightController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BodyWeightController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyWeights()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bodyWeights = await _context.BodyWeights
                .Where(bw => bw.UserId == userId)
                .ToListAsync();
            return Ok(bodyWeights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBodyWeightById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bodyWeight = await _context.BodyWeights
                .FirstOrDefaultAsync(bw => bw.Id == id && bw.UserId == userId);
            if (bodyWeight == null)
            {
                return NotFound();
            }
            return Ok(bodyWeight);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBodyWeight([FromBody] BodyWeight bodyWeight)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bodyWeight.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BodyWeights.Add(bodyWeight);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBodyWeightById), new { id = bodyWeight.Id }, bodyWeight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBodyWeight(int id, [FromBody] BodyWeight bodyWeight)
        {
            if (id != bodyWeight.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (bodyWeight.UserId != userId)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(bodyWeight).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BodyWeights.Any(bw => bw.Id == id && bw.UserId == userId))
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
        public async Task<IActionResult> DeleteBodyWeight(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bodyWeight = await _context.BodyWeights
                .FirstOrDefaultAsync(bw => bw.Id == id && bw.UserId == userId);
            if (bodyWeight == null)
            {
                return NotFound();
            }

            _context.BodyWeights.Remove(bodyWeight);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
