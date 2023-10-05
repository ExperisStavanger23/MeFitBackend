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
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {

            try
            {
                var exe = await _context.Exercises.Where(p => p.Id == id).FirstOrDefaultAsync();
                return exe;
            }
            catch
            {
                throw new EntityNotFoundException("exercise", id);
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
                var exercisesToDelete = await _context.Exercises.SingleOrDefaultAsync(p => p.Id == id);

                if (exercisesToDelete == null)
                {
                    throw new EntityNotFoundException(nameof(exercisesToDelete), id);
                }
                else
                {
                    _context.Remove(exercisesToDelete);
                    await _context.SaveChangesAsync();
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
                var exe = await _context.Exercises.SingleOrDefaultAsync(p => p.Id == obj.Id);

                if (exe == null)
                {
                    throw new EntityNotFoundException(nameof(exe), obj.Id);
                }
                else
                {
                    exe!.Name = obj.Name;
                    exe!.Description = obj.Description;
                    exe!.MuscleGroups = obj.MuscleGroups;
                    exe!.Image = obj.Image;
                    exe!.Video = obj.Video;

                    await _context.SaveChangesAsync();
                    return exe!;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
