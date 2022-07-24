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

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.Role))
                    {
                        var resonse = _userService.CreateUserService(user);

                        if (resonse == true)
                        {
                            return Ok(new { responseCode = 201, responseDescription = "User Created Successfully!", Data = user });
                        }

                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }

            return BadRequest(new { Message = "Incorrect User Details!!" });
        }
    }
}
