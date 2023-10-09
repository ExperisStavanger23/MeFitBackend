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
            // MuscleGroup
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

            // Exercise
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Barbell Bench Press",
                    Description = " Lay on your back" +
                "on a flat bench, lower the barbell down in a slow pace to your chest level, and then" +
                "press upwards by extending your arms.",
                    Image = "https://www.verywellfit.com/thmb/V4KJH4idbUskL-xSE85WSe8OsPA=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/man-training-with-weights-in-gym-147486767-c0eece2a50154d04ad521c1c3c391380.jpg",
                    Video = "https://www.youtube.com/embed/rxD321l2svE",
                    Reps = 8,
                    Sets = 4,
                });
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 2,
                    Name = "Situp",
                    Description = "Situps are classic abdominal exercises done by lying on your back and lifting your torso. They use your body weight to strengthen and tone the core-stabilizing abdominal muscles.",
                    Image = "https://images.healthshots.com/healthshots/en/uploads/2022/10/27130441/sit-ups-vs-crunches.jpg",
                    Video = "https://www.youtube.com/embed=UMaZGY6CbC4",
                    Reps = 8,
                    Sets = 4,
                });

            // Role
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleTitle = "Admin"
                },
                new Role
                {
                    Id = 2,
                    RoleTitle = "Contributer"
                },
                new Role
                {
                    Id = 3,
                    RoleTitle = "User"
                });
            // user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Jeff",
                    Email = "jeffit@gmail.com",
                    ExperienceLvl = Level.Advanced,
                    Gender = "Male",
                    Weight = 80,
                    Height = 180,
                    Birthday = new DateTime(1999-12-03),
                    RoleId = 1
                });
            modelBuilder.Entity<Created>().HasData(
                new Created
                {
                    Id = 1,
                    CreatorId = 1,
                    EntityId = 1,
                    EntityType = EntityType.Exercise
                });
            // workout
            modelBuilder.Entity<Workout>().HasData(
                new Workout
                {
                    Id = 1,
                    Name = "Chest Day",
                    Description = "Chest day is a day where you train your chest muscles",
                    Category = WorkoutCategory.BodyWeightTraining,
                    RecommendedLevel = Level.Beginner,
                    Image =
                        "https://www.mensjournal.com/.image/t_share/MTk2MTM2NjcyOTc1NzI2MDg1/afitasianguyinawhitetanktopdoes.jpg",
                    Duration = 60,
                });
        }
    }
}
