using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.Model.Entities;
using Event_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.WebSockets;
using System.Security.Claims;

namespace Event_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(policyName: "PerUserPolicy")]
    public class EventController : ControllerBase
    {

        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("test")]
        [Authorize]

        public IActionResult Test()
        {
            return Ok(_eventService.ExceptionAsync());
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        private string GetRole()
        {
            return User.FindFirst(ClaimTypes.Role).Value;
        }


        [HttpPost]
        [Route("create")]
        //[Authorize(Policy = "AdminOrOrganizer")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> CreateEvent(CreateEventDTO dto)
        {

            var result = await _eventService.CreateEventAsync(dto,GetUserId(),GetRole());
            return Ok(result);
        }


        [HttpPatch]
        [Route("update/{Id}")]
        //[Authorize(Policy = "AdminOrOrganizer")]
        [Authorize(Roles = "Admin,Organizer")]

        public async Task<IActionResult> UpdateEvent(Guid Id, UpdateEventDTO dto)
        {

            var result = await _eventService.UpdateEventAsync(Id, dto, GetUserId(),GetRole());
            return Ok(result);
        }


        [HttpGet]
        [Route("get-all")]
        //[Authorize(Policy = "ForAll")]
        [Authorize(Roles = "Admin,Organizer,Attendee")]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEventsAsync();
            return Ok(result);
        }


        [HttpGet]
        [Route("get/{Id}")]
        //[Authorize(Policy = "AdminOrOrganizer")]
        [Authorize(Roles = "Admin,Organizer,Attendee")]
        public async Task<IActionResult> GetEventById(Guid Id)
        {
            var result = await _eventService.GetEventByIdAsync(Id);
            return Ok(result);
        }


        [HttpGet]
        [Route("get/organizer/{organizerId}")]
        //[Authorize(Policy = "AdminOrOrganizer")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> GetEventsByOrganizerId(Guid organizerId)
        {
            var result = await _eventService.GetEventByOrganizerIdAsync(organizerId);
            return Ok(result);
        }


        [HttpDelete]
        [Route("delete/{Id}")]
        //[Authorize(Policy = "AdminOrOrganizer")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> DeleteEvent(Guid Id)
        {
            var result = await _eventService.DeleteEventAsync(Id, GetUserId(), GetRole());

            return NoContent();
        }

        

        //Registrations 

        [HttpPost]
        [Route("{eventId}/register")]
        [Authorize(Roles = "Attendee")]
        public async Task<IActionResult> RegisterForEvent(Guid eventId)
        {
            await _eventService.RegisterForEventAsync(eventId, GetUserId());
            return Ok("Registered successfully");
        }

        
        [HttpDelete]
        [Route("{eventId}/cancel")]
        [Authorize(Roles = "Attendee")]
        public async Task<IActionResult> CancelRegistration(Guid eventId)
        {
            await _eventService.CancelRegistrationAsync(eventId, GetUserId());
            return Ok("Registration cancelled successfully");
        }


        [HttpGet]
        [Route("registrations")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRegistrations()
        {
            var result = await _eventService.GetAllRegistrationsAsync(GetRole());
            return Ok(result);
        }

        [HttpGet]
        [Route("organizer/{OrganizerId}/registration")]
        [Authorize(Roles = "Admin,Organizer")]

        public async Task<IActionResult> GetRegistrationsByOrganizer(Guid OrganizerId)
        {
            var result = await _eventService.GetRegistrationByOrganizerIdAsync(OrganizerId);
            return Ok(result);
        }









    }
}
