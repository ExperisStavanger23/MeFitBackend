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
            var prog = await _context.Programs.Where(p => p.Id == id)
                .Include(p => p.Workout)
                .FirstAsync();

            if (prog == null)
            {
                throw new EntityNotFoundException(nameof(prog), id);
            }

            return prog;
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
    }
}
