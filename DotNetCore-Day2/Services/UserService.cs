using AutoMapper;
using DotNetCore_Day2.Data;
using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetCore_Day2.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User?> RegisterAsync(UserDTO dto)
        {
            var user = await _context.Users.AnyAsync(u => u.Name == dto.Name);
            if(user)
            {
                return null;
            }
            var creatuser = new User();
            creatuser.Name = dto.Name;
            creatuser.PasswordHash = new PasswordHasher<User>().HashPassword(creatuser, dto.Password);
            creatuser.Role = dto.Role;
           
            await _context.Users.AddAsync(creatuser);
            await _context.SaveChangesAsync();
            return creatuser;
        }

        public async Task<string?> LoginAsync(UserLoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == dto.Name);
            if (user == null) {
                return null;
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user,user.PasswordHash,dto.Password) == PasswordVerificationResult.Failed) {
                return null;
            }


            var token = CreateToken(user);

            return token;
        }
        public string CreateToken(User user) {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Id",user.Id.ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDesctriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDesctriptor);
        }

    }
}
