using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "AtLeast2Restaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] RestaurantQuery restaurantQuery)
        {
            return Ok(_restaurantService.GetAll(restaurantQuery));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            RestaurantDto restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto restaurantDto)
        {
            int restaurantId = _restaurantService.Create(restaurantDto);

            return Created($"/api/restaurant/{restaurantId}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _restaurantService.Delete(id);
          
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AtLeast20")]
        public ActionResult Update([FromRoute]int id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            _restaurantService.Update(id, updateRestaurantDto);

            return Ok();
        }
    }
}
