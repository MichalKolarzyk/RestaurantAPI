﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            DateTime dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            string userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            logger.LogInformation($"User: {userEmail} date of birth: {dateOfBirth}");


            if(dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
