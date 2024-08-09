using Microsoft.AspNetCore.Identity;

namespace YolkedWorkoutLogger.Server.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Workout> Workouts { get; set; } = new List<Workout>();
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<BodyWeight> BodyWeights { get; set; } = new List<BodyWeight>();

    }
}
