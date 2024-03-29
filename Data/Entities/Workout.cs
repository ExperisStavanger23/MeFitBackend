using MeFitBackend.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(Workout))]
    public class Workout
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkoutCategory Category { get; set; }
        public Level RecommendedLevel { get; set; }
        public string Image {  get; set; }
        public int Duration { get; set; }

        // Navigation
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();     
        public ICollection<UserWorkout> UserWorkouts { get; set; } = new List<UserWorkout>();
        public ICollection<Program>? Programs { get; set; }

    }
}
