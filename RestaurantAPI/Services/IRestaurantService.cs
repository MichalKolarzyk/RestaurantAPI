using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto createRestaurantDto);
        void Update(int updateRestaurantid, UpdateRestaurantDto updateRestaurantDto);
        PagedResult<RestaurantDto> GetAll(RestaurantQuery restaurantQuery);
        RestaurantDto GetById(int id);
        void Delete(int id);
    }
}