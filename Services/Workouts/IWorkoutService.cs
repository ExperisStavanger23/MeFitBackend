using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Workouts
{
    public interface IWorkoutService : ICRUDService<Workout, int>
    {
        Task<ICollection<WorkoutExercise>> GetWorkoutExercisesAsync(int id);
        Task UpdateWorkoutExersiesAsync(int id, int[] workoutexerciseIds);
    }
}
