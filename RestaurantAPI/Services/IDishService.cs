using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dishDto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
        void RemoveAll(int restaurantId);
        void Remove(int restaurantId, int dishId);
    }
}