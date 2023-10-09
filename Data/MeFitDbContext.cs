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

        /* ----------------------- Relationship configurations -------------------------- */
        private void ConfigWorkoutExerciseRelation(ModelBuilder modelBuilder)
        {
            // user
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            // userprogram
            modelBuilder.Entity<UserProgram>()
                .HasKey(up => up.Id);
            modelBuilder.Entity<UserProgram>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPrograms)
                .HasForeignKey(up => up.UserId);
            modelBuilder.Entity<UserProgram>()
                .HasOne(up => up.Program)
                .WithMany(p => p.UserPrograms)
                .HasForeignKey(up => up.ProgramId);
            // UserWorkout
            modelBuilder.Entity<UserWorkout>()
                .HasKey(uw => uw.Id);
            modelBuilder.Entity<UserWorkout>()
                .HasOne(uw => uw.User)
                .WithMany( u => u.UserWorkouts)
                .HasForeignKey(uw => uw.UserId);
            modelBuilder.Entity<UserWorkout>()
                .HasOne(uw => uw.Workout)
                .WithMany(w => w.UserWorkouts)
                .HasForeignKey(uw => uw.WorkoutId);

            // uSERexercise
            modelBuilder.Entity<UserExercise>()
                .HasKey(ue => ue.Id);
            modelBuilder.Entity<UserExercise>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserExercises)
                .HasForeignKey(ue => ue.UserId);
            modelBuilder.Entity<UserExercise>()
                .HasOne(ue => ue.Exercise)
                .WithMany(e => e.UserExercises)
                .HasForeignKey(ue => ue.ExerciseId);

            // UserGoal
            modelBuilder.Entity<UserGoal>()
                .HasKey(ug => ug.Id);
            modelBuilder.Entity<UserGoal>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGoal>()
                .HasOne(ug => ug.Goal)
                .WithMany(g => g.UserGoals)
                .HasForeignKey(ug => ug.GoalId);

            // workoutexercise
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => we.Id);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany()
                .HasForeignKey(we => we.ExerciseId);
        }
        /* ----------------------------------------------------------------------- */


        /* -------------------------------- Seeding ------------------------------ */
        // Implement Seed data for entities here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigWorkoutExerciseRelation(modelBuilder);

            // MuscleGroup
            modelBuilder.Entity<MuscleGroup>().HasData(
                new MuscleGroup
                {
                    Id = 1,
                    Name = "Chest",
                },
                new MuscleGroup
                {
                    Id = 2,
                    Name = "Triceps",
                },
                new MuscleGroup
                {
                    Id = 3,
                    Name = "Shoulders",
                }
            );

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
                    Video = "https://www.youtube.com/embed/rxD321l2svE"
                });
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 2,
                    Name = "Situp",
                    Description = "Situps are classic abdominal exercises done by lying on your back and lifting your torso. They use your body weight to strengthen and tone the core-stabilizing abdominal muscles.",
                    Image = "https://images.healthshots.com/healthshots/en/uploads/2022/10/27130441/sit-ups-vs-crunches.jpg",
                    Video = "https://www.youtube.com/embed=UMaZGY6CbC4",
                });

            // Role
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleTitle = "Admin",
                },
                new Role
                {
                    Id = 2,
                    RoleTitle = "Contributer",
                },
                new Role
                {
                    Id = 3,
                    RoleTitle = "User",
                }
            );

            // WorkoutExercise
            modelBuilder.Entity<WorkoutExercise>().HasData(
                new WorkoutExercise
                {
                    Id = 1,
                    WorkoutId = 1, 
                    ExerciseId = 1, 
                    Reps = 8,
                    Sets = 4,
                }
            
            );

            // User
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
                    Birthday = new DateTime(1999 - 12 - 03),
                    Roles = new List<Role>
                    { new Role { Id = 1,}}
                }
            );

            // Created
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
        /* ----------------------------------------------------------------------- */
    }
}
