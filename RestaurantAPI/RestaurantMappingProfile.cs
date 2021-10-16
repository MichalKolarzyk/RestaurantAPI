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


            CreateMap<Dish, DishDto>();
        }
    }
}
