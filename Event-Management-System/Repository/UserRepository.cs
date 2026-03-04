using Event_Management_System.Data;
using Event_Management_System.DTOs;
using Event_Management_System.Model.Entities;
using Event_Management_System.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<User?> GetById(Guid Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }

        public async Task<User?> Register(User user)
        {
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Update(Guid Id, UpdateUserDTO dto)
        {
           var user = await _context.Users.FindAsync(Id);
            user.Name = dto.Name;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<bool?> Delete(User user)
        {
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }
    }
}
