using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.DTO.Workouts;
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
            return await _context.Workouts
                .Include(we => we.WorkoutExercises)
                .ThenInclude( we => we.Exercise)
                .ThenInclude(e => e.ExerciseMuscleGroups)
                .ThenInclude(emg => emg.MuscleGroup)
                .ToListAsync();
        }

        public async Task<Workout> GetByIdAsync(int id)
        {
            try
            {
                var workout = await _context.Workouts
                    .Where(w => w.Id == id)
                    .Include(w => w.WorkoutExercises)
                    .ThenInclude(we => we.Exercise)
                    .ThenInclude(e => e.ExerciseMuscleGroups)
                    .ThenInclude(emg => emg.MuscleGroup)
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

        public async Task<ICollection<WorkoutExercise>> GetWorkoutExercisesAsync(int id)
        {
            if (!await WorkoutExistAsync(id))
            {
                throw new EntityNotFoundException("Workout", id);
            }

            List<WorkoutExercise> workoutexercises = new List<WorkoutExercise>();

            var workouts = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .Where(w => w.Id == id).ToListAsync();

            foreach (var workout in workouts)
            {
                foreach (var workoutexercise in workout.WorkoutExercises)
                {
                    if (!workoutexercises.Contains(workoutexercise))
                    {
                        workoutexercises.Add(workoutexercise);
                    }
                }
            }
            return workoutexercises;
        }

        public async Task UpdateWorkoutExersiesAsync(int id, int[] workoutexerciseIds)
        {
            if (!await WorkoutExistAsync(id))
            {
                throw new EntityNotFoundException("Workout", id);
            }

            List<WorkoutExercise> workoutexersieList = new List<WorkoutExercise>();

            foreach (var wId in workoutexerciseIds)
            {
                if (!await WorkoutExerciseExistAsync(wId))
                {
                    throw new EntityNotFoundException("Workout exersie", id);
                }

                workoutexersieList.Add(_context.WorkoutExercises.Single(m => m.Id == wId));
            }

            var weToUpdate = await _context.Workouts.Include(w => w.WorkoutExercises).SingleAsync(w => w.Id == id); ;
            weToUpdate.WorkoutExercises = workoutexersieList;

            await _context.SaveChangesAsync();
        }

        //public async Task<ICollection<UserWorkout>> GetUserWorkoutAsync(int id)
        //{
        //    if (!await WorkoutExistAsync(id))
        //    {
        //        throw new EntityNotFoundException("Workout", id);
        //    }

        //    List<UserWorkout> userworkouts = new List<UserWorkout>();   

        //    var workouts = await _context.Workouts
        //        .Include(w => w.UserWorkouts)
        //        .Where(w => w.Id == id).ToListAsync();

        //    foreach (var workout in workouts)
        //    {
        //        foreach (var userworkout in workout.UserWorkouts)
        //        {
        //            if (!userworkouts.Contains(userworkout))
        //            {
        //                userworkouts.Add(userworkout);
        //            }
        //        }
        //    }
        //    return userworkouts;
        //}

        //public async Task UpdateUserWorkoutsAsync(int id, int[] userworkoutIds)
        //{
        //    if (!await WorkoutExistAsync(id))
        //    {
        //        throw new EntityNotFoundException("Workout", id);
        //    }
        //    List<UserWorkout> userworkoutList = new List<UserWorkout>();

        //    foreach (var uId in userworkoutIds)
        //    {
        //        if (!await UserWorkoutsExistAsync(id))
        //        {
        //            throw new EntityNotFoundException("User workout", id);
        //        }

        //        userworkoutList.Add(_context.UserWorkouts.Single(u => u.Id == uId));
        //    }

        //    var uwToUpdate = await _context.Workouts.Include(w => w.UserWorkouts).SingleAsync(w => w.Id == id);
        //    uwToUpdate.UserWorkouts = userworkoutList;

        //    await _context.SaveChangesAsync();
        //}

        // Helper functions
        public async Task<bool> WorkoutExistAsync(int id)
        {
            return await _context.Workouts.AnyAsync(w => w.Id == id);
        }

        public async Task<bool> WorkoutExerciseExistAsync(int id)
        {
            return await _context.WorkoutExercises.AnyAsync(w => w.Id == id);
        }

        public async Task<bool> UserWorkoutsExistAsync(int id)
        {
            return await _context.UserWorkouts.AnyAsync(w => w.Id == id);
        }
    }
}
