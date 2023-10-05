using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;


namespace MeFitBackend.Services.Workouts
{
    public class WorkoutService : IWorkoutService
    {
        private readonly MeFitDbContext _context;

        public WorkoutService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Workout>> GetAllAsync()
        {
            return await _context.Workouts.Include(w => w.Exercises).ToListAsync();
        }

        public async Task<Workout> GetByIdAsync(int id)
        {
            try
            {
                var work = await _context.Workouts.Where(w => w.Id == id)
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync();
                return work;
            }
            catch (SqlException ex)
            {
                throw new EntityNotFoundException("Workout", id);
            }
        }

        public async Task<Workout> AddAsync(Workout obj)
        {
            await _context.Workouts.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var workoutToDelete = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == id);

                if (workoutToDelete == null)
                {
                    throw new EntityNotFoundException(nameof(workoutToDelete), id);
                }
                else
                {
                    _context.Remove(workoutToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public async Task<Workout> UpdateAsync(Workout obj)
        {
            try
            {
                var work = await _context.Workouts.SingleOrDefaultAsync(w => w.Id == obj.Id);

                if (work == null)
                {
                    throw new EntityNotFoundException(nameof(work), obj.Id);
                }
                else
                {
                    work!.Name = obj.Name;
                    work!.Description = obj.Description;
                    work!.Category = obj.Category;
                    work!.RecommendedLevel = obj.RecommendedLevel;
                    work!.Image = obj.Image;
                    work!.Duration = obj.Duration;
                    await _context.SaveChangesAsync();
                    return work!;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
