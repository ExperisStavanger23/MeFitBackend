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
            return await _context.Programs.Include(p => p.Workout).ToListAsync();
        }

        public async Task<Program> GetByIdAsync(int id)
        {

            try
            {
                var prog = await _context.Programs.Where(p => p.Id == id)
               .Include(p => p.Workout)
               .FirstOrDefaultAsync();

                return prog;
            }
            catch
            {
                throw new EntityNotFoundException("Program", id);
            }
        }

        public async Task<Program> AddAsync(Program obj)
        {
            await _context.Programs.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
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
                .Include(p => p.Workout)
                .Where(p => p.Id == id)
                .ToListAsync();

            foreach (var program in programs)
            {
                foreach (var workout in program.Workout)
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

            var programToUpdate = await _context.Programs.Include(p => p.Workout).SingleAsync(p => p.Id == id);
            programToUpdate.Workout = workoutList;
        }

        public async Task<ICollection<UserProgram>> GetUserProgramsAsync(int id)
        {
            if (!await ProgramExistsAsync(id))
            {
                throw new EntityNotFoundException("Program", id);
            }

            List<UserProgram> userprograms = new List<UserProgram>();

            var programs = await _context.Programs
                .Include(p => p.UserPrograms)
                .Where(p => p.Id == id)
                .ToListAsync();

            foreach (var program in programs)
            {
                foreach (var userprogram in program.UserPrograms)
                {
                    if (!userprograms.Contains(userprogram))
                    {
                        userprograms.Add(userprogram);
                    }
                }
            }

            return userprograms;
        }

        public async Task UpdateUserProgramsAsync(int id, int[] userprogramIds)
        {
            if (!await ProgramExistsAsync(id))
            {
                throw new EntityNotFoundException("Program", id);
            }

            List<UserProgram> userprogramList = new List<UserProgram>();
            foreach (var wid in userprogramIds)
            {
                if (!await UserProgramExistsAsync(wid))
                {
                    throw new EntityNotFoundException("User programs", wid);
                }

                userprogramList.Add(_context.UserPrograms.Single(w => w.Id == wid));
            }

            var programToUpdate = await _context.Programs.Include(p => p.UserPrograms).SingleAsync(p => p.Id == id);
            programToUpdate.UserPrograms = userprogramList;
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

        public async Task<bool> UserProgramExistsAsync(int id)
        {
            return await _context.UserPrograms.AnyAsync(w => w.Id == id);
        }
    }
}
