using Event_Management_System.DTOs;

namespace Event_Management_System.Services.Interfaces

{
    public interface IUserService
    {
        public Task<List<GetAllUserResponseDTO>> GetAllUsersAsync();
        public Task<CreateUserResponceDTO?> RegisterUserAsync(CreateUserDTO dto);

        public Task<string?> LoginAsync(LoginUserDTO dto);

        public Task<CreateUserResponceDTO?> UpdateUserAsync(Guid Id,UpdateUserDTO dto);
        public Task<bool?> DeleteUserAsync(Guid Id);
    }
}
