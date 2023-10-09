using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Programs
{
    public interface IProgramService : ICRUDService<Program, int>
    {

        Task<ICollection<Workout>> GetWorkoutsAsync(int id);
        Task UpdateWorkoutsAsync(int id, int[] workoutIds);

        // Task<ICollection<UserProgram>> GetUserProgramsAsync(int id);
        // Task UpdateUserProgramsAsync(int id, int[] userprogramIds);
    }
}
