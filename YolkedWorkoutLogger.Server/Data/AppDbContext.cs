using Microsoft.EntityFrameworkCore;

namespace YolkedWorkoutLogger.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships here if needed
            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.Workout)
                .WithMany(w => w.Exercises)
                .HasForeignKey(e => e.WorkoutId);

            modelBuilder.Entity<Set>()
                .HasOne(s => s.Exercise)
                .WithMany(e => e.Sets)
                .HasForeignKey(s => s.ExerciseId);
        }
    }
}
