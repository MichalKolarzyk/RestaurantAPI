using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private RestaurantDbContext _context;
        private IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dishDto)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);

            Dish dish = _mapper.Map<Dish>(dishDto);
            dish.RestaurantId = restaurantId;
            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            Restaurant restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException($"Restourant not found");

            Dish dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.RestaurantId != restaurantId)
                throw new NotFoundException($"Dish not found");

            return _mapper.Map<DishDto>(dish);
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);

            return _mapper.Map<List<DishDto>>(restaurant.Dishes);
        }

        public void RemoveAll(int restaurantId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);
            _context.Dishes.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public void Remove(int restaurantId, int dishId)
        {
            Restaurant restaurant = GetRestaurantById(restaurantId);
            Dish dish = restaurant.Dishes.FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.RestaurantId != restaurantId)
                throw new NotFoundException($"Dish not found");

            _context.Dishes.Remove(dish);

            _context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            Restaurant restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException($"Restourant not found");

            return restaurant;
        }
    }
}
