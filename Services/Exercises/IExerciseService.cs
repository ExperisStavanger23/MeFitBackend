using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Exercises
{
    public interface IExerciseService : ICRUDService<Exercise, int>
    {
        Task<ICollection<MuscleGroup>> GetMuscleGroupsAsync(int id);
        // Task<ICollection<UserExercise>> GetUserExerciseAsync(int id);
        Task UpdateMuscleGroupsAsync(int id, int[] musclegroupIds);
        // Task UpdateUserExercisesAsync(int id, int[] userexerciseIds);
    }
}
