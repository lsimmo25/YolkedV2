using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YolkedWorkoutLogger.Server.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        // Foreign key
        public int WorkoutId { get; set; }

        // Navigation property
        [ForeignKey("WorkoutId")]
        public Workout? Workout { get; set; }

        public List<Set>? Sets { get; set; }
    }
}
