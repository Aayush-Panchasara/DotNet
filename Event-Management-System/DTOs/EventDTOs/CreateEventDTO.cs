using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.DTOs.EventDTOs
{
    public class CreateEventDTO
    {
        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }


        [Required]
        [StringLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }


        [Required]

        public DateTime Date { get; set;}

        [Required]
        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Location { get; set; }

    }
}
