namespace YolkedWorkoutLogger.Server.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Set>? Sets { get; set; }
    }
}
