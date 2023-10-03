using MeFitBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeFitBackend.Data
{
    public class MeFitDbContext: DbContext
    {
        public MeFitDbContext(DbContextOptions<MeFitDbContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Program> Programs { get; set; } = null!;
        public DbSet<Workout> Workouts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Barbell Bench Press",
                    Description = " Lay on your back" +
                "on a flat bench, lower the barbell down in a slow pace to your chest level, and then" +
                "press upwards by extending your arms.",
                    MuscleGroup = "Chest, Tricep, Shoulder",
                    Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.verywellfit.com%2Fhow-to-perform-a-decline-chest-press-4683977&psig=AOvVaw1AsWoqslQYhXrtaQGieg22&ust=1696409560409000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLDN45vA2YEDFQAAAAAdAAAAABAZ",
                    Video = "https://www.youtube.com/watch?v=tuwHzzPdaGc"
                });
        }
    }


}
