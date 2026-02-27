using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;

namespace DotNetCore_Day2.Services
{
    public interface IUserService
    {
        public Task<User?> RegisterAsync(UserDTO dto);
        public Task<string?> LoginAsync(UserLoginDTO dto);

    }
}