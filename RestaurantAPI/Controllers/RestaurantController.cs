using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
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
        IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            return Ok(_restaurantService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            RestaurantDto restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }

        [HttpPost]
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
        public ActionResult Update([FromRoute]int id, [FromBody] UpdateRestaurantDto updateRestaurantDto)
        {
            _restaurantService.Update(id, updateRestaurantDto);

            return Ok();
        }
    }
}
