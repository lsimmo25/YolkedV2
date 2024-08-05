using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YolkedWorkoutLogger.Server.Models;

namespace YolkedWorkoutLogger.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SetController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSets()
        {
            var sets = await _context.Sets.ToListAsync();
            return Ok(sets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSetById(int id)
        {
            var set = await _context.Sets.FirstOrDefaultAsync(s => s.Id == id);
            if (set == null)
            {
                return NotFound();
            }
            return Ok(set);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSet([FromBody] Set set)
        {
            _context.Sets.Add(set);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSetById), new { id = set.Id }, set);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSet(int id, [FromBody] Set set)
        {
            if (id != set.Id)
            {
                return BadRequest();
            }

            _context.Entry(set).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sets.Any(s => s.Id == id))
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
        public async Task<IActionResult> DeleteSet(int id)
        {
            var set = await _context.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
