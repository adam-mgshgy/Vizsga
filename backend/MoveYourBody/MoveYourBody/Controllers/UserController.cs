using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration config;
        public UserController(ApplicationDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.config = config;
        }

        
        [HttpGet("{id}"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult GetById(int id)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>().Where(u => u.Id == id).FirstOrDefault();
                user.PasswordHash = null;

                if (user == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező felhasználó"
                    });
                return Ok(user);
            });
        }
        [HttpPut("image"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult SaveImages(string[] base64, [FromQuery] int userId)
        {
            return this.Run(() =>
            {
                byte[] image = Convert.FromBase64String(base64[0].Split(',')[1]);
                Image newImage = new Image()
                {
                    Id = 0,
                    Image_data = image
                };
                dbContext.Set<Image>().Add(newImage);
                dbContext.SaveChanges();

                var user = dbContext.Set<User>().Where(u => u.Id == userId).FirstOrDefault();
                user.Image_id = dbContext.Set<Image>().Where(i => i.Image_data == image).FirstOrDefault().Id;

                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                return Ok();

            });
        }
        [HttpGet("image"), Authorize(Roles = "Trainer, Admin, User")]                                     
        public ActionResult GetImageById(int image_id)
        {
            return this.Run(() =>
            {
                var image = dbContext.Set<Image>()
                                            .Where(i => i.Id == image_id)
                                            .FirstOrDefault();
                
                return Ok(image);
            });
        }
        [HttpGet("email")]                                     
        public ActionResult EmailExists([FromQuery] string email)
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

                register = new User()
                {
                    Id = user.Id,
                    Full_name = user.Full_name,
                    Email = user.Email,
                    Password = user.Password,
                    Phone_number = user.Phone_number,
                    Location_id = user.Location_id,
                    Role = user.Role,
                };


                dbContext.Set<User>().Add(register);
                dbContext.SaveChanges();

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential("contact.moveyourbody@gmail.com", config.GetSection("Auth").GetSection("password").Value);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Sikeres regisztráció!";
                mail.Body = "<div style='text-align: center'><h1>Kedves " + register.Full_name + "!</h1><hr/><h3>Sikeresen regisztrált a MoveYourBody felületére!</h3><h3>Felhasználóneve a bejelentkezéshez: " + register.Email + "</h3><h2>Jó edzést kíván a MoveYourBody csapata!</h2></div>";
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("contact.moveyourbody@gmail.com", "MoveYourBody");
                mail.To.Add(new MailAddress(register.Email));
                smtpClient.Send(mail);
                register.PasswordHash = null;
                return Ok(register);
            });
        }
        [HttpGet("getTrainer"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult GetTrainer(int training_id)
        {
            return this.Run(() =>
            {
                var training = dbContext.Set<Training>().Where(t => t.Id == training_id).FirstOrDefault();

                var trainer = dbContext.Set<User>().Where(t => t.Id == training.Trainer_id).FirstOrDefault();
                trainer.PasswordHash = null;
                return Ok(trainer);
            });
        }        

        [HttpPost("modify"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult Modify(User user)
        {
            return this.Run(() =>
            {
                if (user.Password == "")
                {
                    user.PasswordHash = dbContext.Set<User>().AsNoTracking().Where(u => u.Id == user.Id).First().PasswordHash;
                }
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                //TODO teszt más!
                user.PasswordHash = null;
                return Ok(user);
            });
        }
        [HttpDelete("image/delete"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult DeleteImage(int id)
        {
            return this.Run(() =>
            {
                
                var image = dbContext.Set<Image>().Where(i => i.Id == id).FirstOrDefault();
                dbContext.Remove(image);
                var user = dbContext.Set<User>().Where(u => u.Image_id == id).FirstOrDefault();
                user.Image_id = 0;
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return Ok();
            });
        }
        [HttpDelete, Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult Delete(User user)
        {
            var delUser = dbContext.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            return this.Run(() =>
            {
                var applications = dbContext.Set<Applicant>().Where(a => a.User_id == delUser.Id).ToList();
                foreach (var application in applications)
                {
                    dbContext.Remove<Applicant>(application); 
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
                delUser.PasswordHash = null;
                return Ok(delUser);
            });
        }
    }
}