using Event_Management_System.Model.Enum;
using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public UserRole Role { get; set; }


        public List<Event> OrganizedEvents { get; set; }

        public List<EventRegistration> RegisterEvents { get; set; }

    }
}
