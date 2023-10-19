using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Exercises
{
    public interface IExerciseService : ICRUDService<Exercise, int>
    {
        Task<ICollection<ExerciseMuscleGroup>> GetMuscleGroupsAsync(int id);
        Task UpdateMuscleGroupsAsync(int id, int[] musclegroupIds);
    }
}
