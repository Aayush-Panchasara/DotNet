using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore_Day2.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
