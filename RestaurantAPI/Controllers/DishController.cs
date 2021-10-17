using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/restaurant/{restaurantId}/dish")]
    public class DishController : Controller
    {
        private IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody]CreateDishDto dish)
        {
            int dishId =_dishService.Create(restaurantId, dish);
            return Created($"api/restaurant/{restaurantId}/dish/{dishId}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto result = _dishService.GetById(restaurantId, dishId);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            List<DishDto> result = _dishService.GetAll(restaurantId);
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishService.RemoveAll(restaurantId);
            return NoContent();
        }


        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _dishService.Remove(restaurantId, dishId);
            return NoContent();
        }
    }
}
