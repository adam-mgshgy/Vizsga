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
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("{id}")]                                     // http://localhost:5000/user/12
        public ActionResult Get(int id)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>()
                                            .Include(u => u.Location)
                                            .Where(u => u.Id == id)
                                            .Select(u => new
                                            {
                                                id = u.Id,
                                                email = u.Email,
                                                password = u.Password,
                                                phone_number = u.Phone_number,
                                                trainer = u.Trainer,
                                                location = u.Location
                                            })
                                            .FirstOrDefault();

                if (user == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező felhasználó"
                    });
                return Ok(user);
            });
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
                    Email = user.Email,
                    Password = user.Password,
                    Phone_number = user.Phone_number,
                    Trainer = user.Trainer,
                    Location = dbContext.Set<Location>().FirstOrDefault(l => l.City_name == user.Location.City_name)
                   
                    
                };
                dbContext.Set<User>().Add(register);//TODO ha ures a tablicsku csak userrel mukodik
                dbContext.SaveChanges();
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