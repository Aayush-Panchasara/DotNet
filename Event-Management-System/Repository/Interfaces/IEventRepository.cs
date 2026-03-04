using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.Model.Entities;

namespace Event_Management_System.Repository.Interfaces
{
    public interface IEventRepository
    {
        public Task<Event?> GetEventById(Guid id);
        public Task<List<Event?>> GetEventByOrganizerId(Guid organizerId);

        public Task<List<Event>> GetAll();
        public Task<Event> Create(Event entity);

        public Task<Event?> Update(Guid Id, DateTime Date, string Location);

        public Task<bool> Delete(Guid Id);


        //Registration


        public Task<EventRegistration> GetRegistration(Guid eventId, Guid userId);

        public Task<List<RegistrationResponseDTO>> GetAllRegistrations();

        public Task<List<RegistrationResponseDTO>> GetRegistrationByOrganizerId(Guid OrganizerId);

        public Task AddRegistration(EventRegistration registration);

        public Task<bool> CancelRegistration(EventRegistration registration);

    }
}
