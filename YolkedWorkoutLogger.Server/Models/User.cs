using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace YolkedWorkoutLogger.Server.Models
{
    public class User : IdentityUser
    {
        public List<Workout> Workouts { get; set; } = new List<Workout>();
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<BodyWeight> BodyWeights { get; set; } = new List<BodyWeight>();
    }
}
