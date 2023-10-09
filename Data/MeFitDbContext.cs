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
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Role> Roles { get; set; }

        /* ----------------------- Relationship configurations -------------------------- */
        private void ConfigWorkoutExerciseRelation(ModelBuilder modelBuilder)
        {
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
                    Description = "Lay on your back " +
                    "on a flat bench, lower the barbell down in a slow pace to your chest level, and then " +
                    "press upwards by extending your arms.",
                    Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.verywellfit.com%2Fhow-to-perform-a-decline-chest-press-4683977&psig=AOvVaw1AsWoqslQYhXrtaQGieg22&ust=1696409560409000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLDN45vA2YEDFQAAAAAdAAAAABAZ",
                    Video = "https://www.youtube.com/watch?v=tuwHzzPdaGc",
                }
            );

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
                    Id = "1",
                    Name = "Jeff",
                    Email = "jeffit@gmail.com",
                    ExperienceLvl = Level.Advanced,
                    Gender = "Male",
                    Weight = 80,
                    Height = 180,
                    Birthday = new DateTime(1999 - 12 - 03),
                    RoleId = 1,
                }
            );

            // Created
            modelBuilder.Entity<Created>().HasData(
                new Created
                {
                    Id = 1,
                    CreatorId = 1,
                    EntityId = 1,
                    EntityType = EntityType.Exercise,
                }
            );
        }
        /* ----------------------------------------------------------------------- */
    }
}
