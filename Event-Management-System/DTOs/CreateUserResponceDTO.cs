using Event_Management_System.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.DTOs
{
    public class CreateUserResponceDTO
    {
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
