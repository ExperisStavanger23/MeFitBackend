using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using MeFitBackend.Data.DTO.UserProgram;
using MeFitBackend.Data.DTO.UserWorkout;

namespace MeFitBackend.Services.Users
{
    public class UserService : IUserService
    {
        private readonly MeFitDbContext _context;

        public UserService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.UserWorkouts).ThenInclude(uw => uw.Workout)
                .Include(u => u.UserExercises).ThenInclude(ue => ue.Exercise)
                .Include(u => u.UserPrograms).ThenInclude(up => up.Program).ThenInclude(p => p.Workouts)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                var usr = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.UserExercises).ThenInclude(ue => ue.Exercise)
                .Include(u => u.UserWorkouts).ThenInclude(uw => uw.Workout)
                .Include(u => u.UserPrograms).ThenInclude(up => up.Program).ThenInclude(p => p.Workouts)
                .FirstOrDefaultAsync();

                return usr;
            }
            catch (SqlException ex)
            {
                throw new EntityNotFoundException("User", id);
            }           
        }

        public async Task<User> AddAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(string id)
        {
            try
            {
                var userToDelete = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

                if (userToDelete == null)
                {
                    throw new EntityNotFoundException(nameof(userToDelete), id);
                } 
                else
                {
                    _context.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                }
            } 
            catch (SqlException ex) 
            {
                throw ex;
            }
        }
        public async Task<User> UpdateAsync(User obj)
        {
            try
            {
                var usr = await _context.Users.SingleOrDefaultAsync(u => u.Id == obj.Id);

                if (usr == null)
                {
                    throw new EntityNotFoundException(nameof(usr), obj.Id);
                }
                else
                {
                    usr!.Name = obj.Name;
                    usr!.Email = obj.Email;
                    usr!.Birthday = obj.Birthday;
                    usr!.Gender = obj.Gender;
                    usr!.Weight = obj.Weight;
                    usr!.Height = obj.Height;
                    usr!.Bio = obj.Bio;
                    usr!.ExperienceLvl = obj.ExperienceLvl;
                    usr!.ProfilePicture = obj.ProfilePicture;
                    await _context.SaveChangesAsync();
                    return usr!;
                }
            } catch (SqlException ex)
            {
                throw ex;
            }
        }


        public async Task UpdateUserRolesAsync(string id, int[] roleIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var userToUpdate = await _context.Users
                .Include(e => e.UserRoles)
                .SingleAsync(e => e.Id == id);


            var userroleList = roleIds.Select(rId =>
            {
                var role = _context.Roles.FirstOrDefault(e => e.Id == rId);
                if (role == null)
                {
                    throw new EntityNotFoundException("Role", rId);
                }

                return new UserRole
                {
                    UserId = id,
                    RoleId = role.Id,
                };
            }).ToList();


            userToUpdate.UserRoles = userroleList;

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserExercise>> GetUserExercisesAsync(string id)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }
            List<UserExercise> userexercises = new List<UserExercise>();

            var users = await _context.Users
                .Include(u => u.UserExercises)
                .Where(u => u.Id == id).ToListAsync();

            foreach (var user in users)
            {
                foreach (var userexercise in user.UserExercises)
                {
                    if (!userexercises.Contains(userexercise))
                    {
                        userexercises.Add(userexercise);
                    }
                }
            }
            return userexercises;
        }

        public async Task UpdateUserExercisesAsync(string id, int[] exerciseIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var mgToUpdate = await _context.Users
                .Include(e => e.UserExercises)
                .SingleAsync(e => e.Id == id);


            var userexerciseList = exerciseIds.Select(eId =>
            {
                var exercise = _context.Exercises.FirstOrDefault(e => e.Id == eId);
                if (exercise == null)
                {
                    throw new EntityNotFoundException("Exercise", eId);
                }

                return new UserExercise
                {
                    UserId = id,
                    ExerciseId = eId,

                };
            }).ToList();

            
            mgToUpdate.UserExercises = userexerciseList;

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserWorkout>> GetUserWorkoutsAsync(string id)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }
            List<UserWorkout> userworkouts = new List<UserWorkout>();

            var workouts = await _context.Users
                .Include(e => e.UserWorkouts)
                .Where(e => e.Id == id).ToListAsync();

            foreach (var workout in workouts)
            {
                foreach (var userworkout in workout.UserWorkouts)
                {
                    if (!userworkouts.Contains(userworkout))
                    {
                        userworkouts.Add(userworkout);
                    }
                }
            }
            return userworkouts;
        }

        public async Task UpdateUserWorkoutsAsync(string id, UserWorkoutPostDTO[] workouts)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var uwToUpdate = await _context.Users
                .Include(e => e.UserWorkouts)
                .SingleAsync(e => e.Id == id);
            
            var userWorkoutList = new List<UserWorkout>();

            foreach (UserWorkoutPostDTO wId in workouts)
            {
                var workout = _context.Workouts.FirstOrDefault(w => w.Id == wId.Id);
                if (workout == null)
                {
                    throw new EntityNotFoundException("Workout", wId.Id);
                }
                
                userWorkoutList.Add(new UserWorkout
                {
                    UserId = id,
                    WorkoutId = wId.Id,
                    Workout = workout,
                    DoneDate = wId.DoneDate,
                });
            }

            uwToUpdate.UserWorkouts = userWorkoutList;

            await _context.SaveChangesAsync();
        }


        public async Task UpdateWorkoutGoal(string id, int uwId, DateTime? datefinished)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var user = await _context.Users
                .Include(u => u.UserWorkouts)
                .SingleAsync(u => u.Id == id);

            var userWorkout = user.UserWorkouts.FirstOrDefault(uw => uw.Id == uwId);

            if (userWorkout != null)
            {
                userWorkout.DoneDate = datefinished;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException("Workout", uwId);
            }
        }


        public async Task<ICollection<UserProgram>> GetUserProgramsAsync(string id)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            List<UserProgram> userprograms = new List<UserProgram>();

            var users = await _context.Users
                .Include(e => e.UserPrograms)
                .Where(e => e.Id == id).ToListAsync();

            foreach (var user in users)
            {
                foreach (var userprogram in user.UserPrograms)
                {
                    if (!userprograms.Contains(userprogram))
                    {
                        userprograms.Add(userprogram);
                    }
                }
            }
            return userprograms;
        }

        public async Task UpdateUserProgramsAsync(string id, UserProgramPutDTO[] userProgramList)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var upToUpdate = await _context.Users
                .Include(e => e.UserPrograms)
                .SingleAsync(e => e.Id == id);

            var userProgramsToAdd = new List<UserProgram>();
            var workoutToAdd = Array.Empty<UserWorkoutPostDTO>();
            foreach (var pId in userProgramList)
            {
                var program = await _context.Programs
                    .Include(p => p.Workouts)
                    .FirstOrDefaultAsync(p => p.Id == pId.Id);

                if (program == null)
                {
                    throw new EntityNotFoundException("Program", pId.Id);
                }
                
                //new to be added
                workoutToAdd = program.Workouts.Select(w => new UserWorkoutPostDTO
                {
                    Id = w.Id,
                    DoneDate = null
                }).ToArray();
                
                userProgramsToAdd.Add(new UserProgram
                {
                    UserId = id,
                    ProgramId = pId.Id,
                    Program = program,
                    StartDate = pId.StartDate,
                    EndDate = pId.EndDate,
                });
            }
            // what user has
            var allUserWorkouts = await GetUserWorkoutsAsync(id);
            UserWorkoutPostDTO[] currentUserWorkouts = Array.Empty<UserWorkoutPostDTO>();
            currentUserWorkouts = allUserWorkouts.Aggregate(currentUserWorkouts, (current, userWorkout) => current.Append(new UserWorkoutPostDTO { Id = userWorkout.WorkoutId, DoneDate = userWorkout.DoneDate }).ToArray());
            // add new workouts to the user
            UserWorkoutPostDTO[] resultWorkouts = currentUserWorkouts.Concat(workoutToAdd).ToArray();
            // Update user workouts for the program within this loop
            await UpdateUserWorkoutsAsync(id, resultWorkouts);
            upToUpdate.UserPrograms = userProgramsToAdd;

            await _context.SaveChangesAsync();
        }
        
        private async Task UpdateUserWorkoutsAsyncForProgram(string id, int[] workoutIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            var uwToUpdate = await _context.Users
                .Include(e => e.UserWorkouts)
                .SingleAsync(e => e.Id == id);

            var userworkoutList = workoutIds.Select(wId =>
            {
                var workout = _context.Workouts.FirstOrDefault(w => w.Id == wId);
                if (workout == null)
                {
                    throw new EntityNotFoundException("Workout", wId);
                }

                return new UserWorkout
                {
                    UserId = id,
                    WorkoutId = wId,
                    Workout = workout,
                };
            }).ToList();

            uwToUpdate.UserWorkouts = userworkoutList;

            await _context.SaveChangesAsync();
        }

        // Helper functions
        public async Task<bool> UserExistAsync(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }

    
}
