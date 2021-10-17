using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        RestaurantDbContext _dbContext;
        IMapper _mapper;
        ILogger<RestaurantService> _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            return _mapper.Map<RestaurantDto>(restaurant);
        }


        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            return _mapper.Map<List<RestaurantDto>>(restaurants);
        }

        public int Create(CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            _dbContext.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with Id {id} DELETE action invoked.");

            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Update(int updateRestaurantId, UpdateRestaurantDto updateRestaurantDto)
        {
            Restaurant restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == updateRestaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            restaurant.Name = updateRestaurantDto.Name;
            restaurant.Description = updateRestaurantDto.Description;
            restaurant.HasDelivery = updateRestaurantDto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
