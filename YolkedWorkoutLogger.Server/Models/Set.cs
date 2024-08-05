using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YolkedWorkoutLogger.Server.Models
{
    public class Set
    {
        public int Id { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int Reps { get; set; }

        // Foreign key
        public int ExerciseId { get; set; }

        // Navigation property
        [ForeignKey("ExerciseId")]
        public Exercise? Exercise { get; set; }
    }
}
