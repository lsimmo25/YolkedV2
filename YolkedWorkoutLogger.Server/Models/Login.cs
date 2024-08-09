using System.ComponentModel.DataAnnotations;

namespace YolkedWorkoutLogger.Server.Models
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
