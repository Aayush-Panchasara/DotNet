using AutoMapper;
using Event_Management_System.Data;
using Event_Management_System.DTOs;
using Event_Management_System.MiddleWares.CustomeException;
using Event_Management_System.Model.Entities;
using Event_Management_System.Repository.Interfaces;
using Event_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Management_System.Services

{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository,IConfiguration configuration,IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<CreateUserResponceDTO?> RegisterUserAsync(CreateUserDTO dto)
        {
            var existingUser = await _repository.GetByEmail(dto.Email);

            if (existingUser != null) {
                throw new BadRequestException("User with the same email already exists.");
            }

            var user = new User();
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

            var createdUser = await _repository.Register(user);           

            return new CreateUserResponceDTO
            {
                Name = createdUser.Name,
                Email = createdUser.Email,
                Role = createdUser.Role
            };

        }

        public async Task<string?> LoginAsync(LoginUserDTO dto)
        {
            var user = await _repository.GetByEmail(dto.Email);

            if (user == null)
            {
                throw new NotFoundException("User with the provided email does not exist.");
            }

            var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password.");
            }

            var token = CreateToken(user);

            return token;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                    audience: _configuration.GetValue<string>("AppSettings:Audience"),
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );


            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<CreateUserResponceDTO?> UpdateUserAsync(Guid Id, UpdateUserDTO dto)
        {
            var existingUser = await _repository.GetById(Id);

            if (existingUser == null) {
                throw new NotFoundException("User with the provided Id does not exist.");
            }

            var updateduser = await _repository.Update(Id,dto);

           
            return new CreateUserResponceDTO
            {
                Name = updateduser.Name,
                Email = updateduser.Email,
                Role = updateduser.Role
            };
        }
        public async Task<bool?> DeleteUserAsync(Guid Id)
        {
            var existingUser = await _repository.GetById(Id);

            if (existingUser == null) {
                throw new NotFoundException("User with the provided Id does not exist.");
            }

            var response = await _repository.Delete(existingUser);

            return response;
        }

        public async Task<List<GetAllUserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _repository.GetAll();

            return _mapper.Map<List<GetAllUserResponseDTO>>(users);
        }
    }
}