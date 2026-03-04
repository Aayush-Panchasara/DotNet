using Event_Management_System.DTOs;
using Event_Management_System.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository.Interfaces
{
    public interface IUserRepository
    {
        public  Task<User?> GetByEmail(string email);

        public  Task<User?> GetById(Guid Id);
       
        public Task<List<User>> GetAll();
        public Task<User?> Register(User user);
        public Task<User?> Update(Guid Id,UpdateUserDTO user);

        public Task<bool?> Delete(User user); 
    }
}
