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
    public class WorkoutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkoutController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workouts = await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkoutById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            workout.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWorkoutById), new { id = workout.Id }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (workout.UserId != userId)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(workout).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Workouts.Any(w => w.Id == id && w.UserId == userId))
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
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workout = await _context.Workouts
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
