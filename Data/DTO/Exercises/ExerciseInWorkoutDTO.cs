namespace MeFitBackend.Data.DTO.Exercises;

/// <summary>
/// This DTO is used to show the exercises in a workout context.
/// </summary>
public class ExerciseInWorkoutDto
{
     public int Id { get; set; }
     public int ExerciseId { get; set; }
     public string Name { get; set; } = null!;
     public int Sets { get; set; }
     public int Reps { get; set; }
}