using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}"), Authorize]                                     // http://localhost:5000/user/12
        public ActionResult GetById(int id)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>()
                                            .Where(u => u.Id == id)
                                            .Select(u => new
                                            {
                                                id = u.Id,
                                                full_name = u.Full_name,
                                                email = u.Email,
                                                password = u.Password,
                                                phone_number = u.Phone_number,
                                                trainer = u.Trainer,
                                                location_id = dbContext.Set<Location>().FirstOrDefault(l => l.Id == u.Location_id).Id
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
        [HttpPut("register")]
        public ActionResult New(User user)
        {
            return this.Run(() =>
            {
            if (dbContext.Set<User>().Any(u => u.Email == user.Email))
                return BadRequest(new
                {
                    ErrorMessage = "A megadott e-mail címmel már történt korábban regisztráció"
                });
                User register = null;
                //var location = dbContext.Set<Location>().FirstOrDefault(l => l.Id == user.Location_id);
                //if (location == null)
                //{
                //    return BadRequest(new
                //    {
                //        ErrorMessage = "A megadott város nem létezik"
                //    });
                //}
                
                    register = new User()
                    {
                        Id = user.Id,
                        Full_name = user.Full_name,
                        Email = user.Email,
                        Password = user.Password,
                        Phone_number = user.Phone_number,
                        Trainer = user.Trainer,
                        Location_id = user.Location_id
                    };
                
                
                dbContext.Set<User>().Add(register);
                dbContext.SaveChanges();
                return Ok(register);
            });
        }
        [HttpGet("getTrainer")]
        public ActionResult GetTrainer(int training_id)
        {
            return this.Run(() =>
            {
                var training = dbContext.Set<Training>().Where(t => t.Id == training_id).FirstOrDefault();

                var trainer = dbContext.Set<User>().Where(t => t.Id == training.Trainer_id).Select(t => new { 
                
                    Id = t.Id,
                    Full_name = t.Full_name,
                    Email = "",
                    Location_id = "",
                    Password = "",
                    Phone_number = "",
                    Trainer = true
                }).FirstOrDefault();

                return Ok(trainer);
            });
        }

        [HttpGet("login")]
        public ActionResult Login([FromQuery] string email, string password) 
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>()
                            .FirstOrDefault(u => u.Email == email && u.Password == password);
                if (user == null)
                {
                    return Unauthorized(new
                    {
                        errorMessage = "Hibás e-mail cím vagy jelszó"
                    });
                }
                return Ok(user);
            });
            //TODO
            //var jwt = new JwtService(config);
            //var token = jwt.GenerateSecurityToken(user.Email, new List<Claim>() { new Claim("LoginId", login.Id.ToString()) });

            //return Ok(new
            //{
            //    token = token
            //});
        }

        [HttpPost("modify")]
        public ActionResult Modify(User user)
        {
            return this.Run(() =>
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //dbContext.Entry(user.Location_id).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                return Ok(user);
            });
        }
        [HttpDelete]
        public ActionResult Delete(User user)
        {
            return this.Run(() =>
            {
                //TODO cancel subscriptions before delete
                dbContext.Remove(user);
                dbContext.SaveChanges();
                return Ok(user);
            });
        }
    }
}