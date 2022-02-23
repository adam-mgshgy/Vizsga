using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service;
using MoveYourBody.Service.Auth;
using MoveYourBody.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("registration/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration config;
        public AuthController(IConfiguration config, ApplicationDbContext dbContext)
        {
            this.config = config;
            this.dbContext = dbContext;
        }       

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>()
                                 .FirstOrDefault(r => r.Email == model.Email);

                if (user == null || !user.CheckPassword(model.Password))
                    return Forbid();

                var jwt = new JwtService(config);
                var token = jwt.GenerateSecurityToken(model.Email, user.Role);
                return Ok(new
                {
                    token = token,
                    userId = user.Id
                });
            });
        }        
    }
}
