using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class TrainingSessionController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public TrainingSessionController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("list")]
        public ActionResult ListByTraining([FromQuery] int trainingId)
        {
            return this.Run(() =>
            {
                var sessions = dbContext.Set<TrainingSession>().Where(s => s.Training_id == trainingId);
                Training training = dbContext.Set<Training>().Where(t => t.Id == trainingId).FirstOrDefault();
                var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault().Full_name;
                return Ok(new { 
                    session = sessions,
                    trainer = trainer,
                    training = training
                });
            });
        }
        [HttpGet("get")]
        public ActionResult GetById([FromQuery] int sessionId)
        {
            return this.Run(() =>
            {
                TrainingSession session = dbContext.Set<TrainingSession>().Where(s => s.Id == sessionId).FirstOrDefault();
                return Ok(session);
            });

        }
        [HttpPut("create")]
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
                    Address_name = session.Address_name,
                    Place_name = session.Place_name,
                };
                dbContext.Set<TrainingSession>().Add(newSession);
                dbContext.SaveChanges();
                return Ok(newSession);
            });
        }
        [HttpPost("modify")]
        public ActionResult Modify(TrainingSession session)
        {
            return this.Run(() =>
            {
                //dbContext.Entry(session.Training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //dbContext.Entry(session.Location).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.Entry(session).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return Ok(session);
            });
        }
        [HttpDelete]
        public ActionResult Delete(TrainingSession session)
        {
            return this.Run(() =>
            {
                //var session = dbContext.Set<TrainingSession>().FirstOrDefault(t => t.Id == trainingSessionId);
                dbContext.Remove(session);
                dbContext.SaveChanges();
                return Ok(session);
            });
        }
    }
}