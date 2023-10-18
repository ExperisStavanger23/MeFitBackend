using MeFitBackend.Data.DTO.Exercises;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Workouts;
/// <summary>
/// Workout DTO for getting a workout by id. this represents a workout in the context of fetching a workout by id.
/// </summary>
public class WorkoutGetByIdDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string RecommendedLevel { get; set; }
    public string? Image { get; set; }
    public int? Duration { get; set; }
    public ICollection<WorkoutExerciseDTO> WorkoutExercises { get; set; }
    public ICollection<UserWorkoutDTO> UserWorkouts { get; set; }
}