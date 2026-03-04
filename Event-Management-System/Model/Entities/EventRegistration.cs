using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management_System.Model.Entities
{
    public class EventRegistration
    {
        [ForeignKey("Attendee")]
        public Guid AttendeeId { get; set; }
        public Guid EventId { get; set; }

        public DateTime RegisterDate { get; set; }


        public User Attendee { get; set; }
        public Event Event { get; set; }
    }
}
