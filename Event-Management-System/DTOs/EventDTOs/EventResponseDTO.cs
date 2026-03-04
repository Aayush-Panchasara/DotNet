using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.DTOs.EventDTOs
{
    public class EventResponseDTO
    {
   
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Location { get; set; }

        public Guid OrganizerId { get; set; }
    }
}
