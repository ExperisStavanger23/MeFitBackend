using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;

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
            return await _context.Users.Include(u => u.Roles).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                var usr = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.Roles)
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

        public async Task<ICollection<UserGoal>> GetUserGoalsAsync(string id)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            List<UserGoal> goals = new List<UserGoal>();

            var users = await _context.Goals
                .Include(e => e.UserGoals)
                .Where(e => e.Id.ToString() == id).ToListAsync();

            foreach (var user in users)
            {
                foreach (var goal in user.UserGoals)
                {
                    if (!goals.Contains(goal))
                    {
                        goals.Add(goal);
                    }
                }
            }
            return goals;
        }

        public async Task UpdateUserGoalsAsync(string id, int[] usergoalIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            List<UserGoal> usergoalList = new List<UserGoal>();

            foreach (var gId in usergoalIds)
            {
                if (!await UserGoalExistsAsync(gId))
                {
                    throw new EntityNotFoundException("Goal", id);
                }

                usergoalList.Add(_context.UserGoals.Single(g => g.Id == gId));
            }

            var goalToUpdate = await _context.Users.Include(u => u.Goals).SingleAsync(u => u.Id == id);
            goalToUpdate.Goals = usergoalList;

            await _context.SaveChangesAsync();
        }


        public async Task<ICollection<Created>> GetCreatedAsync(string id)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            List<Created> created = new List<Created>();

            var users = await _context.Users
                .Include(u => u.Created)
                .Where(u => u.Id == id)
                .ToListAsync();

            foreach (var user in users)
            {
                foreach (var create in user.Created)
                {
                    if (!created.Contains(create))
                    {
                        created.Add(create);
                    }
                }
            }
            return created;
        }

        public async Task UpdateCreatedAsync(string id, int[] createdIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }
            List<Created> createdList = new List<Created>();    

            foreach (var cId in createdIds)
            {
                if(!await CreatedExistAsync(cId))
                {
                    throw new EntityNotFoundException("Created", cId);
                }
                createdList.Add(_context.Created.Single(c => c.Id == cId));
            }

            var createdToUpdate = await _context.Users.Include(c => c.Created).SingleAsync(c => c.Id == id);
            createdToUpdate.Created = createdList;

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
                throw new EntityNotFoundException("User workout", id);
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

        public async Task UpdateUserWorkoutsAsync(string id, int[] workoutIds)
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
                };
            }).ToList();

            uwToUpdate.UserWorkouts = userworkoutList;

            await _context.SaveChangesAsync();
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

        public async Task UpdateUserProgramsAsync(string id, int[] programIds)
        {
            if (!await UserExistAsync(id))
            {
                throw new EntityNotFoundException("User", id);
            }

            List<UserProgram> userprogramList = new List<UserProgram>();

            foreach (var uId in programIds)
            {
                if (!await UserProgramExistAsync(uId))
                {
                    throw new EntityNotFoundException("User program", uId);
                }

                userprogramList.Add(_context.UserPrograms.Single(m => m.Id == uId));
            }

            var upToUpdate = await _context.Users.Include(e => e.UserPrograms).SingleAsync(e => e.Id == id);
            upToUpdate.UserPrograms = userprogramList;

            await _context.SaveChangesAsync();
        }

        // Helper functions
        public async Task<bool> UserExistAsync(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserGoalExistsAsync(int id)
        {
            return await _context.UserGoals.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> CreatedExistAsync(int id)
        {
            return await _context.Created.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserExerciseExistAsync(int id)
        {
            return await _context.UserExercises.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserWorkoutExistAsync(int id)
        {
            return await _context.UserWorkouts.AnyAsync(u => u.Id == id);
        }
        public async Task<bool> WorkoutExistAsync(int id)
        {
            return await _context.Workouts.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserProgramExistAsync(int id)
        {
            return await _context.UserPrograms.AnyAsync(u => u.Id == id);
        }

    }

    
}
