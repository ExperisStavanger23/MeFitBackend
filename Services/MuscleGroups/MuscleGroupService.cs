using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeFitBackend.Services.MuscleGroups
{
    public class MuscleGroupService : IMuscleGroupService
    {
        private readonly MeFitDbContext _context;

        public MuscleGroupService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<MuscleGroup> AddAsync(MuscleGroup obj)
        {
            await _context.MuscleGroups.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MuscleGroup>> GetAllAsync()
        {
            return await _context.MuscleGroups
               .ToListAsync();
        }

        public Task<MuscleGroup> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MuscleGroup> UpdateAsync(MuscleGroup obj)
        {
            throw new NotImplementedException();
        }
    }
}
