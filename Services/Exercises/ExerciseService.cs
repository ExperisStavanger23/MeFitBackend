using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

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
            return await _context.Exercises
                .Include(ex => ex.ExerciseMuscleGroups)
                .ThenInclude( exmg => exmg.MuscleGroup)
                .ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            try
            {
                var exercise = await _context.Exercises
                    .Where(ex => ex.Id == id)
                    .Include(ex => ex.ExerciseMuscleGroups)
                    .ThenInclude(exmg => exmg.MuscleGroup)
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

            try
            {
                var exerciseToDelete = await GetByIdAsync(id);

                if (exerciseToDelete != null)
                {
                    // Remove related entities first
                    foreach (var userExercise in exerciseToDelete.UserExercises.ToList())
                    {
                        _context.UserExercises.Remove(userExercise);
                    }

                    foreach (var workoutExercise in exerciseToDelete.WorkoutExercises.ToList())
                    {
                        _context.WorkoutExercises.Remove(workoutExercise);
                    }

                    foreach (var exerciseMuscleGroup in exerciseToDelete.ExerciseMuscleGroups.ToList())
                    {
                        _context.ExerciseMuscleGroups.Remove(exerciseMuscleGroup);
                    }

                    // EREmoval of actual EXERCISE ENTITY FROM DB CONTEXT
                    _context.Exercises.Remove(exerciseToDelete);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(nameof(exerciseToDelete), id);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<Exercise> UpdateAsync(Exercise obj)
        {
            try
            {
                var exerciseToUpdate = await GetByIdAsync(obj.Id);

                if (exerciseToUpdate != null)
                {
                    // Remove related entities first
                    foreach (var userExercise in exerciseToUpdate.UserExercises.ToList())
                    {
                        _context.UserExercises.Remove(userExercise);
                    }

                    foreach (var workoutExercise in exerciseToUpdate.WorkoutExercises.ToList())
                    {
                        _context.WorkoutExercises.Remove(workoutExercise);
                    }

                    foreach (var exerciseMuscleGroup in exerciseToUpdate.ExerciseMuscleGroups.ToList())
                    {
                        _context.ExerciseMuscleGroups.Remove(exerciseMuscleGroup);
                    }

                    // Update exercise properties only (not entity props)
                    exerciseToUpdate.Name = obj.Name;
                    exerciseToUpdate.Description = obj.Description;
                    exerciseToUpdate.Image = obj.Image;
                    exerciseToUpdate.Video = obj.Video;

                    // Save changes
                    await _context.SaveChangesAsync();
                    return exerciseToUpdate;
                }
                else
                {
                    throw new EntityNotFoundException(nameof(exerciseToUpdate), obj.Id);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<MuscleGroup>> GetMuscleGroupsAsync(int id)
        {
            // check for existance first using the boolean helper method
            if (!await ExerciseExistAsync(id))
            {
                throw new EntityNotFoundException(nameof(Exercise), id);
            }
            var muscleGroups = await _context.ExerciseMuscleGroups
                               .Where(exmg => exmg.ExerciseId == id)
                               .Select(exmg => exmg.MuscleGroup)
                               .ToListAsync();
            return muscleGroups;
        }

        public async Task UpdateMuscleGroupsAsync(int id, int[] musclegroupIds)
        {
            if (!await ExerciseExistAsync(id))
            {
                throw new EntityNotFoundException(nameof(Exercise), id);
            }

            var exercise = await _context.Exercises.FindAsync(id);

            // Clear existing relation
            exercise.ExerciseMuscleGroups.Clear();

            // Add new associations
            foreach (var mgId in musclegroupIds)
            {
                exercise.ExerciseMuscleGroups.Add(new ExerciseMuscleGroup { ExerciseId = id, MuscleGroupId = mgId });
            }

            await _context.SaveChangesAsync();
        }

        //public async Task<ICollection<UserExercise>> GetUserExerciseAsync(int id)
        //{
        //    if (!await ExerciseExistAsync(id))
        //    {
        //        throw new EntityNotFoundException("Exercise", id);
        //    }

        //    List<UserExercise> userexercises = new List<UserExercise>();

        //    var exercises = await _context.Exercises
        //        .Include(e => e.UserExercises)
        //        .Where(e => e.Id == id).ToListAsync();

        //    foreach (var exercise in exercises)
        //    {
        //        foreach (var userexercise in exercise.UserExercises)
        //        {
        //            if (!userexercises.Contains(userexercise))
        //            {
        //                userexercises.Add(userexercise);
        //            }
        //        }
        //    }
        //    return userexercises;
        //}

        //public async Task UpdateUserExercisesAsync(int id, int[] userexerciseIds)
        //{
        //    if (!await ExerciseExistAsync(id))
        //    {
        //        throw new EntityNotFoundException("Exercise", id);
        //    }

        //    List<UserExercise> userexercsieList = new List<UserExercise>();

        //    foreach (var uId in userexerciseIds)
        //    {
        //        if (!await UserExerciseExistsAsync(uId))
        //        {
        //            throw new EntityNotFoundException("User exercise", uId);
        //        }

        //        userexercsieList.Add(_context.UserExercises.Single(m => m.Id == uId));
        //    }

        //    var mgToUpdate = await _context.Exercises.Include(e => e.UserExercises).SingleAsync(e => e.Id == id);
        //    mgToUpdate.UserExercises = userexercsieList;

        //    await _context.SaveChangesAsync();
        //}


        // Helper functions

        public async Task<bool> ExerciseExistAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> MuscleGroupExistsAsync(int id)
        {
            return await _context.MuscleGroups.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> UserExerciseExistsAsync(int id)
        {
            return await _context.UserExercises.AnyAsync(u => u.Id == id);
        }
    }
}
