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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("workouts")]
        public async Task<IActionResult> GetUserWorkouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workouts = await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();

            return Ok(workouts);
        }

        [HttpGet("foods")]
        public async Task<IActionResult> GetUserFoods()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foods = await _context.Foods
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return Ok(foods);
        }

        [HttpGet("bodyweights")]
        public async Task<IActionResult> GetUserBodyWeights()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bodyWeights = await _context.BodyWeights
                .Where(bw => bw.UserId == userId)
                .ToListAsync();

            return Ok(bodyWeights);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetUserSummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workouts = await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();

            var foods = await _context.Foods
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var bodyWeights = await _context.BodyWeights
                .Where(bw => bw.UserId == userId)
                .ToListAsync();

            var summary = new
            {
                Workouts = workouts,
                Foods = foods,
                BodyWeights = bodyWeights
            };

            return Ok(summary);
        }
    }
}
