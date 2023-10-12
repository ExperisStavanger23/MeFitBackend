using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Users
{
    public interface IUserService : ICRUDService<User, string>
    {
        Task<ICollection<UserGoal>> GetUserGoalsAsync(string id);
        Task<ICollection<Created>> GetCreatedAsync(string id);
        Task<ICollection<UserExercise>> GetUserExercisesAsync(string id);
        Task<ICollection<UserWorkout>> GetUserWorkoutsAsync(string id);
        Task<ICollection<UserProgram>> GetUserProgramsAsync(string id);
        Task UpdateUserGoalsAsync(string id, int[] usergoalIds);
        Task UpdateCreatedAsync(string id, int[] createdIds);
        Task UpdateUserExercisesAsync(string id, int[] exerciseIds);
        Task UpdateUserWorkoutsAsync(string id, int[] workoutIds);
        Task UpdateUserProgramsAsync(string id, int[] programIds, DateTime starttime, DateTime endtime);

    }
}
