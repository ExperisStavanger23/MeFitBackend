using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Users
{
    public interface IUserService : ICRUDService<User, int>
    {
        Task<ICollection<UserGoal>> GetUserGoalsAsync(int id);
        Task<ICollection<Created>> GetCreatedAsync(int id);
        Task<ICollection<UserExercise>> GetUserExercisesAsync(int id);
        Task<ICollection<UserWorkout>> GetUserWorkoutsAsync(int id);
        Task<ICollection<UserProgram>> GetUserProgramsAsync(int id);
        Task UpdateUserGoalsAsync(int id, int[] usergoalIds);
        Task UpdateCreatedAsync(int id, int[] createdIds);
        Task UpdateUserExercisesAsync(int id, int[] userexerciseIds);
        Task UpdateUserWorkoutsAsync(int id, int[] userworkoutIds);
        Task UpdateUserProgramsAsync(int id, int[] userprogramIds);

    }
}
