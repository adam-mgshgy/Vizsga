using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class TrainingSessionController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration config;

        public TrainingSessionController(ApplicationDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.config = config;

        }
        [HttpGet("list"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult ListByTraining([FromQuery] int training_id)
        {
            return this.Run(() =>
            {
                var sessions = dbContext.Set<TrainingSession>().Where(s => s.Training_id == training_id).OrderBy(t => t.Date).ToList();
                Training training = dbContext.Set<Training>().Where(t => t.Id == training_id).FirstOrDefault();
                Category category = dbContext.Set<Category>().Where(c => c.Id == training.Category_id).FirstOrDefault();
                var tagIds = dbContext.Set<TagTraining>().Where(t => t.Training_id == training_id).ToList();
                var tags = new List<Tag>();
                foreach (var tag in tagIds)
                {
                    tags.AddRange(dbContext.Set<Tag>().Where(t => t.Id == tag.Tag_id).ToList());
                }
                var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault().Full_name;
                var image_id = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault().Image_id;

                var image = dbContext.Set<Image>().Where(i =>i.Id == image_id).FirstOrDefault();
                return Ok(new { 
                    sessions,
                    trainer,
                    training,
                    image,
                    category,
                    tags
                });
            });
        }
        [HttpGet("applied"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult ListAppliedSessions([FromQuery] int training_id, [FromQuery] int userId)
        {
            return this.Run(() =>
            {
                var applications = dbContext.Set<Applicant>().Where(a => a.User_id == userId).ToList();
                var sessions = new List<TrainingSession>(); 
                foreach (var appl in applications)
                {
                    var sess = dbContext.Set<TrainingSession>().Where(s => s.Training_id == training_id && s.Id == appl.Training_session_id).FirstOrDefault();
                    if (sess != null)
                    {
                        sessions.Add(sess);
                    }
                }
                Training training = dbContext.Set<Training>().Where(t => t.Id == training_id).FirstOrDefault();
                sessions = sessions.OrderBy(s => s.Date).ToList();
                return Ok(new
                {
                    sessions,
                    training
                });
            });
        }
        [HttpGet("get")]
        public ActionResult GetById([FromQuery] int sessionId)
        {
            return this.Run(() =>
            {
                TrainingSession session = dbContext.Set<TrainingSession>().Where(s => s.Id == sessionId).FirstOrDefault();
                Location location = dbContext.Set<Location>().Where(l => l.Id == session.Location_id).FirstOrDefault();
                Training training = dbContext.Set<Training>().Where(t => t.Id == session.Training_id).FirstOrDefault();

                return Ok(new
                {
                    session,
                    training,
                    location
                });
            });

        }
        [HttpPut("create"), Authorize(Roles = "Trainer, Admin")]
        public ActionResult CreateSession(TrainingSession session)
        {
            return this.Run(() =>
            {
                TrainingSession newSession = new TrainingSession
                {
                    Id = 0, /// !!!
                    Training_id = session.Training_id,
                    Location_id = session.Location_id,
                    Date = session.Date,
                    Price = session.Price,
                    Min_member = session.Min_member,
                    Max_member = session.Max_member,
                    Minutes = session.Minutes,
                    Number_of_applicants = 0,
                    Address_name = session.Address_name,
                    Place_name = session.Place_name,
                };
                newSession.Date = newSession.Date.AddHours(1);
                dbContext.Set<TrainingSession>().Add(newSession);
                dbContext.SaveChanges();
                return Ok(newSession);
            });
        }
        [HttpPost("modify"), Authorize(Roles = "Trainer, Admin")]
        public ActionResult Modify(TrainingSession session)
        {
            return this.Run(() =>
            {
                dbContext.Entry(session).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return Ok(session);
            });
        }
        [HttpDelete, Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult Delete(TrainingSession session)
        {
            return this.Run(() =>
            {                
                var applicants = dbContext.Set<Applicant>().Where(a => a.Training_session_id == session.Id).ToList();

                var training = dbContext.Set<Training>().Where(t => t.Id == session.Training_id).FirstOrDefault();
                var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential("contact.moveyourbody@gmail.com", config.GetSection("Auth").GetSection("password").Value);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.Subject = "Edzés lemondva!";
                mail.Body = "<div style='text-align: center;'><h1>Kedves Jelentkező!</h1><h3> Az edzés, amire jelentkezett lemondásra került! </h3><hr><h1>" + training.Name + "</h1><h2>" + session.Date + "</h2><h4>" + trainer.Full_name + "</h4><h4>" + training.Contact_phone + "</h4></div>";
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("contact.moveyourbody@gmail.com", "MoveYourBody");
                foreach (var applicant in applicants)
                {
                    dbContext.Remove<Applicant>(applicant);
                    var user = dbContext.Set<User>().Where(u => u.Id == applicant.User_id).FirstOrDefault();
                    mail.To.Add(new MailAddress(user.Email));
                    smtpClient.Send(mail);
                }
                dbContext.Remove(session);
                dbContext.SaveChanges();
                return Ok(session);
            });
        }
    }
}