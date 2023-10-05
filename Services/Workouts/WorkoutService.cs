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
                var workout = await _context.Workouts
                    .Where(w => w.Id == id)
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync();

                if (workout == null)
                {
                    throw new EntityNotFoundException(nameof(workout), id);
                }
                return workout;
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
                var workoutToDelete = await GetByIdAsync(id);

                if (workoutToDelete != null)
                {
                    // have to delete related entities containg a fk with ref to the parent entity
                    workoutToDelete.UserWorkouts.Clear();
                    _context.Remove(workoutToDelete);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new EntityNotFoundException(nameof(workoutToDelete), id);
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
                var workoutToUpdate = await GetByIdAsync(obj.Id);

                if (workoutToUpdate != null)
                {
                    // clear related entity with FK to it
                    workoutToUpdate.UserWorkouts.Clear();

                    // update props
                    workoutToUpdate!.Name = obj.Name;
                    workoutToUpdate!.Description = obj.Description;
                    workoutToUpdate!.Category = obj.Category;
                    workoutToUpdate!.RecommendedLevel = obj.RecommendedLevel;
                    workoutToUpdate!.Image = obj.Image;
                    workoutToUpdate!.Duration = obj.Duration;

                    // add back 
                    foreach( var userWorkout in obj.UserWorkouts )
                    {
                        workoutToUpdate.UserWorkouts.Add( userWorkout );
                    }
                    await _context.SaveChangesAsync();
                    return workoutToUpdate!;

                }
                else
                {
                    throw new EntityNotFoundException(nameof(workoutToUpdate), obj.Id);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
