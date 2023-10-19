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
            return await _context.Exercises.Include(e => e.ExerciseMuscleGroups)
                .ThenInclude(emg => emg.MuscleGroup)
                .ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            try
            {
                var exercise = await _context.Exercises.Where(e => e.Id == id).Include(e => e.ExerciseMuscleGroups)
                .ThenInclude(emg => emg.MuscleGroup).FirstOrDefaultAsync();
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
                    // Update exercise properties only (not entity props)
                    exerciseToUpdate.Name = obj.Name;
                    exerciseToUpdate.Description = obj.Description;
                    exerciseToUpdate.Image = obj.Image;
                    exerciseToUpdate.Video = obj.Video;

                    // Remove related entities (excluding EXMG)
                    foreach (var userExercise in exerciseToUpdate.UserExercises.ToList())
                    {
                        _context.UserExercises.Remove(userExercise);
                    }

                    foreach (var workoutExercise in exerciseToUpdate.WorkoutExercises.ToList())
                    {
                        _context.WorkoutExercises.Remove(workoutExercise);
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

        public async Task<ICollection<ExerciseMuscleGroup>> GetMuscleGroupsAsync(int id)
        {
            // check for existance first using the boolean helper method
            if (!await ExerciseExistAsync(id))
            {
                throw new EntityNotFoundException(nameof(Exercise), id);
            }
            var muscleGroups = await _context.ExerciseMuscleGroups
                               .Where(exmg => exmg.ExerciseId == id)
                               .Include( emg => emg.MuscleGroup)
                               .ToListAsync();
            return muscleGroups;
        }

        public async Task UpdateMuscleGroupsAsync(int id, int[] musclegroupIds)
        {
            var exercise = await GetByIdAsync(id);
            if (exercise == null)
            {
                throw new EntityNotFoundException(nameof(Exercise), id);
            }
            var existingAssociations = exercise.ExerciseMuscleGroups.ToList();

            // Remove associations that are not in the new list
            foreach (var existingAssociation in existingAssociations)
            {
                if (!musclegroupIds.Contains(existingAssociation.MuscleGroupId))
                {
                    _context.ExerciseMuscleGroups.Remove(existingAssociation);
                }
            }

            // Add new associations that do not already exist
            foreach (var mgId in musclegroupIds)
            {
                if (!existingAssociations.Any(ea => ea.MuscleGroupId == mgId))
                {
                    var mg = await _context.MuscleGroups.FindAsync(mgId);
                    if (mg != null)
                    {
                        exercise.ExerciseMuscleGroups.Add(new ExerciseMuscleGroup { ExerciseId = id, MuscleGroupId = mgId });
                    }
                }
            }


            await _context.SaveChangesAsync();
        }


        // Helper functions

        public async Task<bool> ExerciseExistAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }
    }
}
