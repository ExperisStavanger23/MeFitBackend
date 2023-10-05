using Microsoft.EntityFrameworkCore;
using MeFitBackend.Data;
using MeFitBackend.Data.Entities;
using MeFitBackend.Data.Exceptions;
using Microsoft.Data.SqlClient;

namespace MeFitBackend.Services.Users
{
    public class UserService : IUserService
    {
        private readonly MeFitDbContext _context;

        public UserService(MeFitDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var usr = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.Role)
                .FirstOrDefaultAsync();

                return usr;
            }
            catch (SqlException ex)
            {
                throw new EntityNotFoundException("User", id);
            }           
        }

        public async Task<User> AddAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var userToDelete = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

                if (userToDelete == null)
                {
                    throw new EntityNotFoundException(nameof(userToDelete), id);
                } 
                else
                {
                    _context.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                }
            } 
            catch (SqlException ex) 
            {
                throw ex;
            }
        }
        public async Task<User> UpdateAsync(User obj)
        {
            try
            {
                var usr = await _context.Users.SingleOrDefaultAsync(u => u.Id == obj.Id);

                if (usr == null)
                {
                    throw new EntityNotFoundException(nameof(usr), obj.Id);
                }
                else
                {
                    usr!.Name = obj.Name;
                    usr!.Email = obj.Email;
                    usr!.Birthday = obj.Birthday;
                    usr!.Gender = obj.Gender;
                    usr!.Weight = obj.Weight;
                    usr!.Height = obj.Height;
                    usr!.Bio = obj.Bio;
                    usr!.ProfilePicture = obj.ProfilePicture;
                    await _context.SaveChangesAsync();
                    return usr!;
                }
            } catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
