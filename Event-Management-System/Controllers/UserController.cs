using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Event_Management_System.Services.Interfaces;
using Event_Management_System.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Event_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDTO dto)
        {
            var user = await _userService.RegisterUserAsync(dto);

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginUserDTO dto)
        {
            var token = await _userService.LoginAsync(dto);

            return Ok(token);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok("This endpoint is only accessible to Admins.");
        }

        [HttpPatch]
        [Route("update/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid Id, UpdateUserDTO dto)
        {
            var user = await _userService.UpdateUserAsync(Id, dto);
            return Ok(user);
        }

        [HttpDelete]
        [Route("delete/{Id}")]
        [Authorize(Policy = "AdminOnly")]

        public async Task<IActionResult> Deleteuser(Guid Id) {
        
            var response = await _userService.DeleteUserAsync(Id);

            return Ok("User Deleted Successfully.");
        }

        [HttpGet]
        [Route("get-all")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }
    }
    
}
