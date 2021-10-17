using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        Stopwatch _stopwathc;
        ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwathc = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwathc.Start();
            await next.Invoke(context);
            _stopwathc.Stop();

            long milisecounds = _stopwathc.ElapsedMilliseconds;
            if(milisecounds > 4000)
            {
                string message = $"Request {context.Request.Method} at {context.Request.Path} took {milisecounds}ms";
                _logger.LogInformation(message);
            }
        }
    }
}
