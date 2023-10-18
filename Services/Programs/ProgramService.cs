using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;


namespace MeFitBackend.Services.Programs
{
    public class ProgramService : IProgramService
    {
        private readonly MeFitDbContext _context;

        public ProgramService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Program>> GetAllAsync()
        {
            return await _context.Programs.Include(p => p.Workouts)
                .ThenInclude(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
                .ThenInclude(e => e.ExerciseMuscleGroups)
                .ThenInclude(emg => emg.MuscleGroup)
                .ToListAsync();
        }

        public async Task<Program> GetByIdAsync(int id)
        {

            try
            {
                return await _context.Programs.Where(p => p.Id == id)
                    .Include(p => p.Workouts) // Include the workouts
                        .ThenInclude(w => w.WorkoutExercises) // Include the workout exercises
                        .ThenInclude(we => we.Exercise) // Include the exercises
                        .ThenInclude(e => e.ExerciseMuscleGroups) // Include the exercise muscle groups
                        .ThenInclude(emg => emg.MuscleGroup) // Include the muscle group
                        .FirstOrDefaultAsync();
            }
            catch
            {
                throw new EntityNotFoundException("Program", id);
            }
        }


        public async Task<Program> AddAsync(Program program, int[] workoutIds)
        {
            var workouts = await _context.Workouts
                .Where(w => workoutIds.Contains(w.Id))
                .ToListAsync();
            program.Workouts = workouts;

            await _context.Programs.AddAsync(program);
            await _context.SaveChangesAsync();

            return program;
        }

        public async Task DeleteByIdAsync(int id)
        {

            try
            {
                var programsToDelete = await _context.Programs.SingleOrDefaultAsync(p => p.Id == id);

                if (programsToDelete == null)
                {
                    throw new EntityNotFoundException(nameof(programsToDelete), id);
                }
                else
                {
                    _context.Remove(programsToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<Program> UpdateAsync(Program obj)
        {
            try
            {
                var prog = await _context.Programs.SingleOrDefaultAsync(p => p.Id == obj.Id);

                if (prog == null)
                {
                    throw new EntityNotFoundException(nameof(prog), obj.Id);
                }
                else
                {
                    prog!.Name = obj.Name;
                    prog!.Duration = obj.Duration;
                    prog!.Description = obj.Description;
                    prog!.Category = obj.Category;
                    prog!.RecommendedLevel = obj.RecommendedLevel;
                    prog!.Image = obj.Image;

                    await _context.SaveChangesAsync();
                    return prog!;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Workout>> GetWorkoutsAsync(int id)
        {
            if (!await ProgramExistsAsync(id))
            {
                throw new EntityNotFoundException("Program", id);
            }

            List<Workout> workouts = new List<Workout>();

            var programs = await _context.Programs
                .Include(p => p.Workouts)
                .Where(p => p.Id == id)
                .ToListAsync();

            foreach (var program in programs)
            {
                foreach (var workout in program.Workouts)
                {
                    if (!workouts.Contains(workout))
                    {
                        workouts.Add(workout);
                    }
                }
            }

            return workouts;
        }

        public async Task UpdateWorkoutsAsync(int id, int[] workoutIds)
        {
            if (!await ProgramExistsAsync(id))
            {
                throw new EntityNotFoundException("Program", id);
            }

            List<Workout> workoutList = new List<Workout>();
            foreach (var wid in workoutIds)
            {
                if (!await WorkoutExistsAsync(wid))
                {
                    throw new EntityNotFoundException("Workout", wid);
                }

                workoutList.Add(_context.Workouts.Single(w => w.Id == wid));
            }

            var programToUpdate = await _context.Programs.Include(p => p.Workouts).SingleAsync(p => p.Id == id);
            
            programToUpdate.Workouts = workoutList;
            await _context.SaveChangesAsync();
        }

        public async Task<Program> AddAsync(Program program)
        {
            await _context.Programs.AddAsync(program);
            await _context.SaveChangesAsync();
            return program;
        }
        public async Task<Program> GetProgramWithWorkoutsAsync(int programId)
        {
            return await _context.Programs
                .Include(p => p.Workouts)
                .Where(p => p.Id == programId)
                .FirstOrDefaultAsync();
        }


        // helper functions

        public async Task<bool> ProgramExistsAsync(int id)
        {
            return await _context.Programs.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> WorkoutExistsAsync(int id)
        {
            return await _context.Workouts.AnyAsync(w => w.Id == id);
        }
    }
}
