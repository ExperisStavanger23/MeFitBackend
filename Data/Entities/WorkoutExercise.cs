using System.ComponentModel.DataAnnotations.Schema;

namespace MeFitBackend.Data.Entities
{
    [Table(nameof(WorkoutExercise))]
    public class WorkoutExercise
    {
        public int Id { get; set; }

        // FKs
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }

        // Specs; reps n sets
        public int Reps { get; set; }
        public int Sets { get; set; }

        // Navs
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
