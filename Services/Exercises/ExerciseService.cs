using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;

namespace MeFitBackend.Services.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private readonly MeFitDbContext _context;

        public ExerciseService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Exercise>> GetAllAsync()
        {
            return await _context.Exercises.Include(ex => ex.MuscleGroup).ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            try
            {
                var exercise = await _context.Exercises
                    .Where(ex => ex.Id == id)
                    .Include(ex => ex.MuscleGroup)
                    .FirstOrDefaultAsync();

                if (exercise == null)
                {
                    throw new EntityNotFoundException(nameof(exercise), id);
                }

                return exercise;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<Exercise> AddAsync(Exercise obj)
        {
            await _context.Exercises.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {

            // try
            // {
            //     var exerciseToDelete = await GetByIdAsync(id);

            //     if (exerciseToDelete != null)
            //     {
            //         // removing related entities cause of fk constain
            //         exerciseToDelete.MuscleGroup.Clear();
            //         exerciseToDelete.UserExercises.Clear();
            //         _context.Exercises.Remove(exerciseToDelete);
            //         await _context.SaveChangesAsync();
            //     }
            //     else
            //     {
            //         throw new EntityNotFoundException(nameof(exerciseToDelete), id);
            //     }
            // }
            // catch (SqlException ex)
            // {
            //     throw ex;
            // }
        }

        public async Task<Exercise> UpdateAsync(Exercise obj)
        {
            throw new NotImplementedException();
            // try
            // {
            //     var exerciseToUpdate = await GetByIdAsync(obj.Id);

            //     if (exerciseToUpdate != null)
            //     {
            //         // Clear related entities
            //         exerciseToUpdate.MuscleGroups.Clear();
            //         exerciseToUpdate.UserExercises.Clear();

            //         // Update exercise properties only
            //         exerciseToUpdate.Name = obj.Name;
            //         exerciseToUpdate.Description = obj.Description;
            //         exerciseToUpdate.Image = obj.Image;
            //         exerciseToUpdate.Video = obj.Video;
            //         //exerciseToUpdate.Reps = obj.Reps;
            //         //exerciseToUpdate.Sets = obj.Sets;

            //         // add back
            //         foreach (var muscleGroup in obj.MuscleGroups)
            //         {
            //             exerciseToUpdate.MuscleGroups.Add(muscleGroup);
            //         }
            //         foreach( var exercise in obj.UserExercises)
            //         {
            //             exerciseToUpdate.Exercises.Add(userExercise);
            //         }

            //         // Save changes
            //         await _context.SaveChangesAsync();
            //         return exerciseToUpdate;
            //     }
            //     else
            //     {
            //         throw new EntityNotFoundException(nameof(exerciseToUpdate), obj.Id);
            //     }
            // }
            // catch (SqlException ex)
            // {
            //     throw ex;
            // }
        }

        // public async Task<ICollection<MuscleGroup>> GetMuscleGroupsAsync(int id)
        // {
        //     if (!await ExerciseExistAsync(id))
        //     {
        //         throw new EntityNotFoundException("Exercise", id);
        //     }

        //     List<MuscleGroup> muscleGroups = new List<MuscleGroup>();

        //     var exercises = await _context.Exercises
        //         .Include(e => e.MuscleGroups)
        //         .Where(e => e.Id == id).ToListAsync();

        //     foreach (var exercise in exercises)
        //     {
        //         foreach (var musclegroup in exercise.MuscleGroups)
        //         {
        //             if (!muscleGroups.Contains(musclegroup))
        //             {
        //                 muscleGroups.Add(musclegroup);
        //             }
        //         }
        //     }
        //     return muscleGroups;
        // }

        public async Task UpdateMuscleGroupsAsync(int id, int[] musclegroupIds)
        {
            // if (!await ExerciseExistAsync(id))
            // {
            //     throw new EntityNotFoundException("Exercise", id);
            // }

            // List<MuscleGroup> musclegroupList = new List<MuscleGroup>();

            // foreach (var mId in musclegroupIds)
            // {
            //     if (!await MuscleGroupExistsAsync(mId))
            //     {
            //         throw new EntityNotFoundException("Muscle group", mId);
            //     }

            //     musclegroupList.Add(_context.MuscleGroups.Single(m => m.Id == mId));
            // }

            // var mgToUpdate = await _context.Exercises.Include(e => e.MuscleGroup).SingleAsync(e => e.Id == id);
            // mgToUpdate.MuscleGroup = musclegroupList;

            // await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Exercise>> GetUserExerciseAsync(string id)
        {
            // if (!await UserE(id))
            // {
            //     throw new EntityNotFoundException("Exercise", id);
            // }


            var user = await  _context.Users.Include(u => u.Exercises).SingleOrDefaultAsync(u => u.Id == id);
            return user.Exercises;
        }

        // public async Task UpdateUserExercisesAsync(int id, int[] userexerciseIds)
        // {
        //     if (!await ExerciseExistAsync(id))
        //     {
        //         throw new EntityNotFoundException("Exercise", id);
        //     }

        //     List<UserExercise> userexercsieList = new List<UserExercise>();

        //     foreach (var uId in userexerciseIds)
        //     {
        //         if (!await UserExerciseExistsAsync(uId))
        //         {
        //             throw new EntityNotFoundException("User exercise", uId);
        //         }

        //         userexercsieList.Add(_context.UserExercises.Single(m => m.Id == uId));
        //     }

        //     var mgToUpdate = await _context.Exercises.Include(e => e.UserExercises).SingleAsync(e => e.Id == id);
        //     mgToUpdate.UserExercises = userexercsieList;

        //     await _context.SaveChangesAsync();
        // }


        // Helper functions

        public async Task<bool> ExerciseExistAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> MuscleGroupExistsAsync(int id)
        {
            return await _context.MuscleGroups.AnyAsync(m => m.Id == id);
        }

        // public async Task<bool> UserExerciseExistsAsync(int id)
        // {
        //     return await _context.UserExercises.AnyAsync(u => u.Id == id);
        // }
    }
}
