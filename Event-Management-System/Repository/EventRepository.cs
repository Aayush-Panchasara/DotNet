using Event_Management_System.Data;
using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.Model.Entities;
using Event_Management_System.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Repository
{
    public class EventRepository: IEventRepository
    {
        private readonly AppDBContext _context;

        public EventRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Event> Create(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Event> Update(Guid Id, DateTime Date, string Location)
        {
            var eventToUpdate = await GetEventById(Id);
            eventToUpdate.Date = Date;
            eventToUpdate.Location = Location;
            await _context.SaveChangesAsync();

            return eventToUpdate;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var eventToDelete = await GetEventById(Id);

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Event?> GetEventById(Guid id)
        {
            var events = await _context.Events.AsNoTracking().Include(e => e.RegisterUsers).FirstOrDefaultAsync(e => e.Id == id);
            if (events == null)
            {
                return null;
            }
            return events;
        }

        public async Task<List<Event?>> GetEventByOrganizerId(Guid organizerId)
        {
            var events = await _context.Events.AsNoTracking().Include(e => e.RegisterUsers).Where(e => e.OrganizerId == organizerId).ToListAsync();
            if (events == null)
            {
                return null;
            }
            return events;
        }

        public async Task<List<Event>> GetAll()
        {
            var events = await _context.Events.AsNoTracking().ToListAsync();
            if (events == null)
            {
                return null;
            }
            return events;
        }


        //Registrations 

        public async Task<EventRegistration> GetRegistration(Guid eventId, Guid userId)
        {
            var registration = await _context.EventRegistrations.AsNoTracking()
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.AttendeeId == userId);

            if (registration == null) {
                return null;
            }

            return registration;
        }

        public async Task AddRegistration(EventRegistration registration)
        {
            await _context.EventRegistrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CancelRegistration(EventRegistration registration)
        {
            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RegistrationResponseDTO>> GetAllRegistrations()
        {
            var registrations = await _context.EventRegistrations.AsNoTracking()
                                .Include(e => e.Attendee)
                                .Include(e => e.Event)
                                .ToListAsync();

            if(registrations == null) {
                return null;
            }
            return registrations.Select(r => new RegistrationResponseDTO
            {
                EventId = r.EventId,
                EventName = r.Event.Name,
                AttendeeId = r.AttendeeId,
                AttendeeName = r.Attendee.Name,
                AttendeeEmail = r.Attendee.Email,
                RegisterDate = r.RegisterDate
            }).ToList();
        }

        public async Task<List<RegistrationResponseDTO>> GetRegistrationByOrganizerId(Guid OrganizerId)
        {
            var registrations = await _context.EventRegistrations.AsNoTracking()
                .Include(r => r.Attendee)
                .Include(r => r.Event)
                .Where(r => r.Event.OrganizerId == OrganizerId)
                .ToListAsync();

            if (registrations == null)
            {
                return null;
            }

            return registrations.Select(r => new RegistrationResponseDTO
            {
                EventId = r.EventId,
                EventName = r.Event.Name,
                AttendeeId = r.AttendeeId,
                AttendeeName = r.Attendee.Name,
                AttendeeEmail = r.Attendee.Email,
                RegisterDate = r.RegisterDate
            }).ToList();
        }

    }
}
