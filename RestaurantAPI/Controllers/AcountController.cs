using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AcountController : Controller
    {
        private IAccountService _accountService;

        public AcountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto loginDto)
        {
            string token = _accountService.GenerateJwt(loginDto);
            return Ok(token);
        }
    }
}
