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
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }

        /* ----------------------------------- Custom relationship configurations ----------------------------- */
        /* -------------- WorkoutExercise Relation ----------------- */
        private void CustomRelationConfigs(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => we.Id);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId);
        }
        /* --------------------------------------------------------- */
        /* ---------------------------------------------------------------------------------------------------- */


        /* ------------------------------------- Seeding methods for entities --------------------------------- */
        /* ------------------------- <Roles> ----------------------- */
        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleTitle = "Admin",
                },
                new Role
                {
                    Id = 2,
                    RoleTitle = "Contributor",
                },
                new Role
                {
                    Id = 3,
                    RoleTitle = "User",
                }
            );
        }
        /* --------------------------------------------------------- */

        /* ------------------------- <Users> ----------------------- */
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    Name = "Jeff",
                    Email = "jeffit@gmail.com",
                    ExperienceLvl = Level.Advanced,
                    Gender = "Male",
                    Weight = 80,
                    Height = 180,
                    Birthday = new DateTime(1999, 12, 3),
                }
            );
        }
        /* --------------------------------------------------------- */

        /* --------------------- <MuscleGroups> -------------------- */
        private void SeedMuscleGroups(ModelBuilder modelBuilder)
        {
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
                },
                new MuscleGroup
                {
                    Id = 4,
                    Name = "Upper Back"
                },
                new MuscleGroup
                {
                    Id = 5,
                    Name = "Lower Back"
                },
                new MuscleGroup
                {
                    Id = 6,
                    Name = "Biceps"
                },
                new MuscleGroup
                {
                    Id = 7,
                    Name = "Triceps"
                },
                new MuscleGroup
                {
                    Id = 8,
                    Name = "Quads"
                },
                new MuscleGroup
                {
                    Id = 9,
                    Name = "Hamstrings"
                },
                new MuscleGroup
                {
                    Id = 10,
                    Name = "Calves"
                },
                new MuscleGroup
                {
                    Id = 11,
                    Name = "Glutes"
                },
                new MuscleGroup
                {
                    Id = 12,
                    Name = "Hips"
                },
                new MuscleGroup
                {
                    Id = 13,
                    Name = "Abs"
                }
            );
        }
        /* --------------------------------------------------------- */

        /* ----------------------- <Exercises> --------------------- */
        private void SeedExercises(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Barbell Bench Press",
                    // assign with chest, triceps, shoulders
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
                    // assign with abs
                    Description = "Situps are classic abdominal exercises done by lying on your back and lifting your torso. They use your body weight to strengthen and tone the core-stabilizing abdominal muscles.",
                    Image = "https://images.healthshots.com/healthshots/en/uploads/2022/10/27130441/sit-ups-vs-crunches.jpg",
                    Video = "https://www.youtube.com/embed=UMaZGY6CbC4",
                });
        }
        /* --------------------------------------------------------- */

        /* ------------------------- <Workouts> -------------------- */

        private void SeedWorkouts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workout>().HasData(
                new Workout
                {
                    Id = 1,
                    Name = "Chest Day",
                    Description = "Chest day is a day where you train your chest muscles",
                    Category = WorkoutCategory.BodyWeightTraining,
                    RecommendedLevel = Level.Beginner,
                    Image = "https://www.mensjournal.com/.image/t_share/MTk2MTM2NjcyOTc1NzI2MDg1/afitasianguyinawhitetanktopdoes.jpg",
                    Duration = 60,
                    
                });
        }
        /* --------------------------------------------------------- */

        /* ----------------------- <Programs> ---------------------- */
        private void SeedPrograms(ModelBuilder modelBuilder)
        {
            // Method for seeding Program entity
            modelBuilder.Entity<Program>().HasData(
                new Program
                {
                    Id = 1,
                    Name = "Upper Body Program",
                    Description = "Get bigger upper body",
                    Category = ProgramCategory.MuscleGain,
                    RecommendedLevel = Level.Beginner,
                    Duration = 14,
                });
        }
        /* --------------------------------------------------------- */

        /* ----------------------- <Created> ----------------------- */
        private void SeedCreateds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Created>().HasData(
                new Created
                {
                    Id = 1,
                    CreatorId = 1,
                    EntityId = 1,
                    EntityType = EntityType.Exercise
                });
        }
        /* --------------------------------------------------------- */

        /* ------------------- <WorkoutExercises> ------------------ */
        private void SeedWorkoutExercises(ModelBuilder modelBuilder)
        {
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
        }
        /* ------------------------------------------------------- */

        /* --------------------- <Goals> ------------------------- */
        private void SeedGoals(ModelBuilder modelBuilder)
        {
            // Method for seeding Goal entity
        }
        /* ------------------------------------------------------- */

        /* ------------------- <UserExercises> ------------------- */
        private void SeedUserExercises(ModelBuilder modelBuilder)
        {
            // Method for seeding UserExercise entity
        }
        /* ------------------------------------------------------- */

        /* ------------------- <UserWorkouts> -------------------- */
        private void SeedUserWorkouts(ModelBuilder modelBuilder)
        {
            // Method for seeding UserWorkout entity
        }
        /* ------------------------------------------------------- */

        /* ------------------- <UserPrograms> -------------------- */
        private void SeedUserPrograms(ModelBuilder modelBuilder)
        {
            // Method for seeding UserProgram entity
        }
        /* ------------------------------------------------------- */

        /* --------------------- <UserGoals> --------------------- */
        private void SeedUserGoals(ModelBuilder modelBuilder)
        {
            // Method for seeding UserGoal entity
        }
        /* ------------------------------------------------------- */

        /* ---------------------------------------------------------------------------------------------------- */


        /* ------------------------------------------- Seeding ------------------------------------------------ */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CustomRelationConfigs(modelBuilder);

            /* ---- Call seeding methods for each entity here ---- */
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedMuscleGroups(modelBuilder);
            SeedExercises(modelBuilder);
            SeedWorkouts(modelBuilder);
            SeedPrograms(modelBuilder);
            SeedCreateds(modelBuilder);
            SeedWorkoutExercises(modelBuilder);
            SeedGoals(modelBuilder);
            SeedUserExercises(modelBuilder);
            SeedUserWorkouts(modelBuilder);
            SeedUserPrograms(modelBuilder);
            SeedUserGoals(modelBuilder);
            /* ---- End of Seeding ---- */
        }
    }
        /* ---------------------------------------------------------------------------------------------------- */
}
