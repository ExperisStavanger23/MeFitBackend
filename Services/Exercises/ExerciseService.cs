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
            return await _context.Exercises.Include(ex => ex.MuscleGroups).ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            try
            {
                var exercise = await _context.Exercises
                    .Where(ex => ex.Id == id)
                    .Include(ex => ex.MuscleGroups)
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
                    // removing related entities cause of fk constain
                    exerciseToDelete.MuscleGroups.Clear();
                    exerciseToDelete.UserExercises.Clear();
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
                    // Clear related entities
                    exerciseToUpdate.MuscleGroups.Clear();
                    exerciseToUpdate.UserExercises.Clear();

                    // Update exercise properties only
                    exerciseToUpdate.Name = obj.Name;
                    exerciseToUpdate.Description = obj.Description;
                    exerciseToUpdate.Image = obj.Image;
                    exerciseToUpdate.Video = obj.Video;
                    exerciseToUpdate.Reps = obj.Reps;
                    exerciseToUpdate.Sets = obj.Sets;

                    // add back
                    foreach (var muscleGroup in obj.MuscleGroups)
                    {
                        exerciseToUpdate.MuscleGroups.Add(muscleGroup);
                    }
                    foreach( var userExercise in obj.UserExercises)
                    {
                        exerciseToUpdate.UserExercises.Add(userExercise);
                    }

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

    }
}
