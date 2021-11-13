using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreatedRequirementHandler : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
    {
        IRestaurantService _restaurantService;
        public MinimumRestaurantsCreatedRequirementHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreatedRequirement requirement)
        {

            return Task.CompletedTask;
        }
    }
}
