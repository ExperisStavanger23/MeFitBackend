namespace MeFitBackend.Data.DTO.Exercises;

/// <summary>
/// This DTO is used to show the Exercise in a Workout context.
/// </summary>
public class WorkoutExerciseDTO
{
    /// <summary>
    /// Respective Workout.Id
    /// </summary>
     public int WorkoutId { get; set; }
    /// <summary>
    /// Respective Exercise.Id
    /// </summary>
     public int ExerciseId { get; set; }
    /// <summary>
    /// 
    /// </summary>
     public int Sets { get; set; }
    /// <summary>
    /// </summary>
     public int Reps { get; set; }
    /// <summary>
    /// The associative ExerciseDTO
    /// </summary>
    public ExerciseDTO Exercise { get; set; } = null!;
}