using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore_Day2.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
