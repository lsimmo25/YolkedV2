using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YolkedWorkoutLogger.Server.Models;

namespace YolkedWorkoutLogger.Server.Controllers
{
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
            var bodyWeights = await _context.BodyWeights.ToListAsync();
            return Ok(bodyWeights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBodyWeightById(int id)
        {
            var bodyWeight = await _context.BodyWeights.FirstOrDefaultAsync(bw => bw.Id == id);
            if (bodyWeight == null)
            {
                return NotFound();
            }
            return Ok(bodyWeight);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBodyWeight([FromBody] BodyWeight bodyWeight)
        {
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
                if (!_context.BodyWeights.Any(bw => bw.Id == id))
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
            var bodyWeight = await _context.BodyWeights.FindAsync(id);
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
