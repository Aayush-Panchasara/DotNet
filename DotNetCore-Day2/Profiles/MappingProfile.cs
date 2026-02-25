using AutoMapper;
using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;

namespace DotNetCore_Day2.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductsDTO>();
            CreateMap<ProductsDTO, Product>();
        }
        
    }
}
