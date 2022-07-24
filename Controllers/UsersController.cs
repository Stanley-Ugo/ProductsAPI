using Microsoft.AspNetCore.Mvc;
using ProductsAPI.ServiceLayer.IServices;
using ProductsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public IActionResult GetAllUsers()
        {
            var response = _userService.GetAllUsersService();

            return Ok(response);
        }
    }
}
