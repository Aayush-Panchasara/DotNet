using AutoMapper;
using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.MiddleWares.CustomeException;
using Event_Management_System.Model.Entities;
using Event_Management_System.Repository.Interfaces;
using Event_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using System.Security.Claims;
namespace Event_Management_System.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepo, IMapper mapper)
        {
            _eventRepository = eventRepo;
            _mapper = mapper;
        }

        public async Task ExceptionAsync()
        {
            throw new NotFoundException("This is a custom exception for testing purpose.");
        }

        public async Task<EventResponseDTO?> CreateEventAsync(CreateEventDTO dto, Guid OrganizerId, string Role)
        {
            if (Role != "Admin" && Role != "Organizer")
            {
                throw new UnauthorizedAccessException("You are not authorized to Organize the event.");
            }

            if(dto.Date < DateTime.UtcNow)
            {
                throw new BadRequestException("Event date cannot be in the past.");
            }

            var Event = new Event
            {
                Name = dto.Name,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                OrganizerId = OrganizerId,
            };

            var createdEvent = await _eventRepository.Create(Event);

            return new EventResponseDTO
            {
                Name = createdEvent.Name,
                Description = createdEvent.Description,
                Date = createdEvent.Date,
                Location = createdEvent.Location,
                OrganizerId = createdEvent.OrganizerId
            };

        }

        public async Task<EventResponseDTO?> UpdateEventAsync(Guid Id, UpdateEventDTO dto, Guid UserId, string Role)
        {

            var result = new Event();

            var eventToUpdate = await _eventRepository.GetEventById(Id);
            if (eventToUpdate == null)
            {
                throw new NotFoundException("Event does not exists.");
            }

            if (dto.Date < DateTime.UtcNow)
            {
                throw new BadRequestException("Event date cannot be in the past.");
            }

            if (Role != "Admin" && eventToUpdate.OrganizerId != UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this event.");
            }

            if (Role == "Admin" || eventToUpdate.OrganizerId == UserId)
            {
                result = await _eventRepository.Update(Id, dto.Date, dto.Location);
            }
            return _mapper.Map<EventResponseDTO>(result);
        }

        public async Task<bool> DeleteEventAsync(Guid Id, Guid UserId, string Role)
        {

            var result = false;

            var eventToDelete = await _eventRepository.GetEventById(Id);
            if (eventToDelete == null)
            {
                throw new NotFoundException("Event does not exists.");
            }

            if (Role != "Admin" && eventToDelete.OrganizerId != UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this event.");
            }

            if (Role == "Admin" || eventToDelete.OrganizerId == UserId)
            {
                result = await _eventRepository.Delete(Id);
            }
            return result;
        }

        public async Task<EventResponseDTO?> GetEventByIdAsync(Guid Id)
        {
            var user = await _eventRepository.GetEventById(Id);
            if (user == null)
            {
                throw new NotFoundException("No event Found");
            }
            return _mapper.Map<EventResponseDTO>(user);
        }

        public async Task<List<EventResponseDTO>> GetEventByOrganizerIdAsync(Guid organizerId)
        {
            var events = await _eventRepository.GetEventByOrganizerId(organizerId);
            if (events == null || events.Count == 0)
            {
                throw new NotFoundException("No event Found");
            }
            return _mapper.Map<List<EventResponseDTO>>(events);
        }

        public async Task<List<EventResponseDTO>> GetAllEventsAsync()
        {
            var evnets = await _eventRepository.GetAll();
            if (evnets == null || evnets.Count == 0)
            {
                throw new NotFoundException("No event Found");

            }
            return _mapper.Map<List<EventResponseDTO>>(evnets);
        }


        //Registration 

        public async Task<EventRegistration> RegisterForEventAsync(Guid eventId, Guid userId)
        {
            var eventExists = await _eventRepository.GetEventById(eventId);
            if (eventExists == null) { 
                throw new NotFoundException("Event not found");
            }

            if (eventExists.Date < DateTime.UtcNow)
            {
                throw new BadRequestException("Cannot register for past events");
            }

            if(eventExists.OrganizerId == userId)
            {
                throw new BadRequestException("You are the one who is organized the event");
            }

            var existingRegistration =
                await _eventRepository.GetRegistration(eventId, userId);

            if (existingRegistration != null)
            { 
                throw new BadRequestException("Already registered for this event"); 
            }

            var registration = new EventRegistration
            {
                EventId = eventId,
                AttendeeId = userId,
                RegisterDate = DateTime.UtcNow
            };

            await _eventRepository.AddRegistration(registration);
                return registration;
        }


        public async Task<bool> CancelRegistrationAsync(Guid eventId, Guid userId)
    {
        var registration =
            await _eventRepository.GetRegistration(eventId, userId);

        if (registration == null)
        {
                throw new BadRequestException("You are not registered for this event"); 
        }

        return await _eventRepository.CancelRegistration(registration);
    }


        public async Task<List<RegistrationResponseDTO>> GetAllRegistrationsAsync(string role)
        {
            if (role != "Admin")
            {
                throw new UnauthorizedAccessException("You are not authorized to view all registrations.");
            }

            var registrations =  await _eventRepository.GetAllRegistrations();

            if (registrations == null || registrations.Count == 0)
                throw new NotFoundException("No registrations found");

            return registrations;

        }

        public async Task<List<RegistrationResponseDTO>> GetRegistrationByOrganizerIdAsync(Guid OrganizerId)
        {
            var events = await _eventRepository.GetRegistrationByOrganizerId(OrganizerId);

            return events;
        }













    }
}