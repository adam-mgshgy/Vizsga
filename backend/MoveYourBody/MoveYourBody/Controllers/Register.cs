using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public RegisterController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        

        [HttpPut]
        public ActionResult New(User user)
        {
            return this.Run(() =>
            {
                if (dbContext.Set<User>().Any(u => u.Email == user.Email))
                    return BadRequest(new
                    {
                        ErrorMessage = "A megadott e-mail címmel már történt korábban regisztráció"
                    });


                var register = new User()
                {
                    Id = user.Id,
                    Full_name = user.Full_name,
                    Email = user.Email                    
                };
                dbContext.Set<User>().Add(register);               
                //dbContext.SaveChanges();
                return Ok(new
                {
                    id = register.Id,
                    email = register.Email,
                    full_name = register.Full_name
                });
            });
        }
    }
}