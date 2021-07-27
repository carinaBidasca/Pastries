using tema3.Models;
using PastriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastriesCommon.Entities;
using AuthorizeAttribute = tema3.Models.AuthorizeAttribute;
using System;

namespace tema3.Controllers
{
    /// <summary>
    /// Authenticate users
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private  IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Login([FromBody] AuthenticateRequest login)
        {
            
            IActionResult response = Unauthorized();
            var token = _userService.Authenticate(login);
            if (token != null)
                response = Ok(token);//token=response

            return response;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllAsync();
            return Ok(users);
        }
       
        [Route("/newToken")]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult RenewToken([FromBody]AuthenticateRequest login)
        {
            string username = login.Username;
            //ar trebui validata si parola
            User user = _userService.GetByUsername(username);
            IActionResult response = Unauthorized();
            var token = _userService.GenerateJwtToken(user);
            if (token != null)
                response = Ok(token);
            return response;

        }
    }
}
