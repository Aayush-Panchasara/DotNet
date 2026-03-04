using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.DTOs.EventDTOs
{
    public class UpdateEventDTO
    {
        [Required]
        public DateTime Date 
        { get; 
          set;
        }
        [Required]
        public string Location { get; set; }
    }
}
