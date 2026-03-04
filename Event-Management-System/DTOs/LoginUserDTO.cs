using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
