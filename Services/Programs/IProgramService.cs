using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Programs
{
    public interface IProgramService : ICRUDService<Program, int>
    {

        Task<ICollection<Workout>> GetWorkoutsAsync(int id);
        Task UpdateWorkoutsAsync(int id, int[] workoutIds);
        Task<Program> AddAsync(Program program);
        Task<Program> AddAsync(Program program, int[] workoutIds);
        Task<Program> GetProgramWithWorkoutsAsync(int programId);
    }
}
