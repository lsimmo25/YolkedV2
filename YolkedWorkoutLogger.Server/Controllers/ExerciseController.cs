using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YolkedWorkoutLogger.Server.Models;

namespace YolkedWorkoutLogger.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExerciseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _context.Exercises.Include(e => e.Sets).ToListAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var exercise = await _context.Exercises.Include(e => e.Sets).FirstOrDefaultAsync(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            _context.Entry(exercise).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Exercises.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
