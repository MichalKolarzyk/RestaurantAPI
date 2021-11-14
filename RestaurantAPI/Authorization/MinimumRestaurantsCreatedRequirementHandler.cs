using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreatedRequirementHandler : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
    {
        RestaurantDbContext _restaurantDbContext;
        ILogger<MinimumRestaurantsCreatedRequirementHandler> _logger;

        public MinimumRestaurantsCreatedRequirementHandler(RestaurantDbContext restaurantDbContext,
            ILogger<MinimumRestaurantsCreatedRequirementHandler> logger)
        {
            _restaurantDbContext = restaurantDbContext;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreatedRequirement requirement)
        {
            int userId = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            string userName = context.User.FindFirst(u => u.Type == ClaimTypes.Name).Value;

            int createdRestaurantsNumber = _restaurantDbContext.Restaurants.Count(r => r.CreatedById == userId);
            _logger.LogInformation($"Login user {userName}");

            if (createdRestaurantsNumber >= requirement.MinRestaurantsCreated)
            {
                _logger.LogInformation("Authorization succeed");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization faild");
            }

            return Task.CompletedTask;
        }
    }
}
