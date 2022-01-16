using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service;
using MoveYourBody.Service.Auth;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Authorize]
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext, IJwtAuth jwtAuth)
        {
            this.dbContext = dbContext;
            this.jwtAuth = jwtAuth;
        }

        private readonly IJwtAuth jwtAuth;       

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var email = dbContext.Set<User>().Where(e => e.Email == userCredential.UserName).FirstOrDefault().Email;
            var password = dbContext.Set<User>().Where(e => e.Password == userCredential.Password).FirstOrDefault().Password;

            if (email != null && password != null)
            {
                var token = jwtAuth.Authentication(email, password);
                if (token == null)
                    return Unauthorized();
                return Ok(token);
            }
            return null;
        }


    }
}