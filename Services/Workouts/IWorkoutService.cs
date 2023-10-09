using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Workouts
{
    public interface IWorkoutService : ICRUDService<Workout, int>
    {
        Task<ICollection<WorkoutExercise>> GetWorkoutExercisesAsync(int id);
        Task<ICollection<UserWorkout>> GetUserWorkoutAsync(int id);
        Task UpdateWorkoutExersiesAsync(int id, int[] workoutexerciseIds);
        Task UpdateUserWorkoutsAsync(int id, int[] userworkoutIds);
    }
}
