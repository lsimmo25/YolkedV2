using Microsoft.EntityFrameworkCore;

namespace YolkedWorkoutLogger.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<BodyWeight> BodyWeights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships between User and related entities

            // User-Workout relationship
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(w => w.UserId);

            // User-Food relationship
            modelBuilder.Entity<Food>()
                .HasOne(f => f.User)
                .WithMany(u => u.Foods)
                .HasForeignKey(f => f.UserId);

            // User-BodyWeight relationship
            modelBuilder.Entity<BodyWeight>()
                .HasOne(bw => bw.User)
                .WithMany(u => u.BodyWeights)
                .HasForeignKey(bw => bw.UserId);

            // Exercise-Workout relationship
            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.Workout)
                .WithMany(w => w.Exercises)
                .HasForeignKey(e => e.WorkoutId);

            // Set-Exercise relationship
            modelBuilder.Entity<Set>()
                .HasOne(s => s.Exercise)
                .WithMany(e => e.Sets)
                .HasForeignKey(s => s.ExerciseId);
        }
    }
}
