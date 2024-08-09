namespace YolkedWorkoutLogger.Server.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        // Foreign Key
        public string UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}
