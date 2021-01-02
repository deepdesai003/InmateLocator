using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhiladelphiaInmateLocator.WebApi.Models;
using PhiladelphiaInmateLocator.WebApi.Services.Interface;
using WebApi.Models.Users;

namespace PhiladelphiaInmateLocator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if(user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: Users/Details/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return await _userService.GetAllUsers() ;
        }
    }
}