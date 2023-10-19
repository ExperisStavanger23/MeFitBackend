using MeFitBackend.Data.DTO.UserProgram;
using MeFitBackend.Data.DTO.UserWorkout;
using MeFitBackend.Data.Entities;

namespace MeFitBackend.Services.Users
{
    public interface IUserService : ICRUDService<User, string>
    {
        Task<ICollection<UserExercise>> GetUserExercisesAsync(string id);
        Task<ICollection<UserWorkout>> GetUserWorkoutsAsync(string id);
        Task<ICollection<UserProgram>> GetUserProgramsAsync(string id);
        Task UpdateUserRolesAsync(string id, int[] roleIds);
        Task UpdateWorkoutGoal(string id, int wId, DateTime? datefinished);
        Task UpdateUserExercisesAsync(string id, int[] exerciseIds);
        Task UpdateUserWorkoutsAsync(string id, UserWorkoutPostDTO[] workouts);
        Task UpdateUserProgramsAsync(string id, UserProgramPutDTO[] userProgramList);

    }
}
