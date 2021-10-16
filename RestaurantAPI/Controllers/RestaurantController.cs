using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        RestaurantDbContext _dbContext;
        IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantDtos = _mapper.Map<RestaurantDto>(restaurants);

            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int id)
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurants == null)
                return NotFound();

            var restaurantDtos = _mapper.Map<RestaurantDto>(restaurants);

            return Ok(restaurantDtos);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]Restaurant restaurant)
        {

        }
    }
}
