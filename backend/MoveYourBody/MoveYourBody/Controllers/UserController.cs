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

        
        [HttpGet("{id}")]                                     // http://localhost:5000/user/12
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
                                                location_id = dbContext.Set<Location>().FirstOrDefault(l => l.Id == u.Location_id).Id,
                                                role = u.Role
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
        [HttpGet("email")]                                     // http://localhost:5000/user/12
        public ActionResult CheckEmail([FromQuery] string email)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>().Where(u => u.Email == email).FirstOrDefault();

                if (user == null)
                {
                    return Ok(false);
                }
                return Ok(true);
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
                    Location_id = user.Location_id,
                    Role = user.Role
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

        [HttpGet("login"), Authorize(Roles = "Admin, Trainer, User")]
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

        [HttpPost("modify"), Authorize(Roles = "Admin, Trainer, User")]
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
        [HttpDelete]//, Authorize(Roles = "Admin, Trainer, User")]
        public ActionResult Delete(User user)
        {
            var delUser = dbContext.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            return this.Run(() =>
            {
                var applications = dbContext.Set<Applicant>().Where(a => a.User_id == delUser.Id).ToList();
                foreach (var application in applications)
                {
                    dbContext.Remove<Applicant>(application); //TODO email kuldes 
                }
                var trainings = dbContext.Set<Training>().Where(t => t.Trainer_id == delUser.Id).ToList();
                var sessions = new List<TrainingSession>();
                foreach (var training in trainings)
                {
                    sessions.AddRange(dbContext.Set<TrainingSession>().Where(s => s.Training_id == training.Id).ToList());
                    dbContext.Remove<Training>(training);
                }
                foreach (var session in sessions)
                {
                    dbContext.Remove<TrainingSession>(session);
                }
                
                dbContext.Remove(delUser);
                dbContext.SaveChanges();
                return Ok(user);
            });
        }
    }
}