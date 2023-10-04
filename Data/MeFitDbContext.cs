using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace MeFitBackend.Data
{
    public class MeFitDbContext: DbContext
    {
        public MeFitDbContext(DbContextOptions<MeFitDbContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserExercise> UserExercises { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<UserProgram> UserPrograms { get; set; }
        public DbSet<UserGoal> UserGoals { get; set; }
        public DbSet<Created> Created { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<MuscleGroup>().HasData(
                new MuscleGroup { 
                    Id = 1, Name = "Chest" ,
                    ExerciseId = 1
                },
                new MuscleGroup { 
                    Id = 2, Name = "Triceps",
                    ExerciseId = 1
                },
                new MuscleGroup {
                    Id = 3, Name = "Shoulders",
                    ExerciseId = 1
                });


            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Barbell Bench Press",
                    Description = " Lay on your back" +
                "on a flat bench, lower the barbell down in a slow pace to your chest level, and then" +
                "press upwards by extending your arms.",
                    Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.verywellfit.com%2Fhow-to-perform-a-decline-chest-press-4683977&psig=AOvVaw1AsWoqslQYhXrtaQGieg22&ust=1696409560409000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLDN45vA2YEDFQAAAAAdAAAAABAZ",
                    Video = "https://www.youtube.com/watch?v=tuwHzzPdaGc",
                    Reps = 8,
                    Sets = 4,
                });
        }
    }
}
