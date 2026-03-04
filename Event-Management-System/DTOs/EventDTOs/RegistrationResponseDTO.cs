namespace Event_Management_System.DTOs.EventDTOs
{
    public class RegistrationResponseDTO
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public Guid AttendeeId { get; set; }
        public string AttendeeName { get; set;}
        public string AttendeeEmail { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
