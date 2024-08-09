namespace YolkedWorkoutLogger.Server.Models
{
    public class BodyWeight
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}
