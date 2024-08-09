namespace YolkedWorkoutLogger.Server.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Calories { get; set; }
        public DateTime? Date { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}
