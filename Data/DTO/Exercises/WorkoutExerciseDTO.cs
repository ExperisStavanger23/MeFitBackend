namespace MeFitBackend.Data.DTO.Exercises;

/// <summary>
/// This DTO is used to show the exercises in a workout context.
/// </summary>
public class WorkoutExerciseDTO
{
     public int WorkoutId { get; set; }
     public int ExerciseId { get; set; }
     public int Sets { get; set; }
     public int Reps { get; set; }
     public ExerciseDTO Exercise { get; set; }
}