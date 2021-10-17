using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(s => s.City, d => d.MapFrom(d => d.Address.City))
                .ForMember(s => s.Street, d => d.MapFrom(d => d.Address.Street))
                .ForMember(s => s.PostalCode, d => d.MapFrom(d => d.Address.PostalCode));

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address()
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<UpdateRestaurantDto, Restaurant>()
                .ForAllMembers(r => r.Ignore());

            CreateMap<Dish, DishDto>();
            CreateMap<CreateDishDto, Dish>();
            

        }
    }
}
