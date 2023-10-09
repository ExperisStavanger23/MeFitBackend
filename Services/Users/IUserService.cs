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
        Task UpdateUserExercisesAsync(string id, int[] userexerciseIds);
        Task UpdateUserWorkoutsAsync(string id, int[] userworkoutIds);
        Task UpdateUserProgramsAsync(string id, int[] userprogramIds);

    }
}
