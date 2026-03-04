using AutoMapper;
using Event_Management_System.DTOs;
using Event_Management_System.DTOs.EventDTOs;
using Event_Management_System.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<User, CreateUserDTO>();
            //CreateMap<CreateUserDTO, User>()
            //    .ForMember(dest => dest.PasswordHash,
            //                opt => opt.MapFrom(src => new PasswordHasher<User>())
            //                );
            CreateMap<User, GetAllUserResponseDTO>();
            CreateMap<Event, EventResponseDTO>();


        }
    }
}
