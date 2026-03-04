using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.Model.Entities;

namespace Event_Management_System.Services.Interfaces
{
    public interface IEventService
    {
        public Task ExceptionAsync();
        public Task<EventResponseDTO?> CreateEventAsync(CreateEventDTO dto,Guid OrganizerId, string Role);
        public Task<List<EventResponseDTO>> GetEventByOrganizerIdAsync(Guid OrganizerId);
        public Task<EventResponseDTO?> GetEventByIdAsync(Guid Id);

        public Task<List<EventResponseDTO>> GetAllEventsAsync();


        public Task<EventResponseDTO?> UpdateEventAsync(Guid Id,UpdateEventDTO dto,Guid UserId, string Role);

        public Task<bool> DeleteEventAsync(Guid Id, Guid UserId, string Role);


        //Registration
        public Task<EventRegistration> RegisterForEventAsync(Guid EventId, Guid UserId);

        public Task<bool> CancelRegistrationAsync(Guid EventId, Guid UserId);

        public Task<List<RegistrationResponseDTO>> GetAllRegistrationsAsync(string Role);

        public Task<List<RegistrationResponseDTO>> GetRegistrationByOrganizerIdAsync(Guid OrganizerId);
    }
}
