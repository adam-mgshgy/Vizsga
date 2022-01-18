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
                //var _sessions = dbContext.Set<TrainingSession>().Where(t => t.Training_id == trainingId);
                //var _training = dbContext.Set<Training>().Where(t => t.Id == trainingId).FirstOrDefault();
                //var _trainerName = dbContext.Set<User>().Where(u => u.Id == _training.Trainer_id).FirstOrDefault().Full_name;
                //var data = new
                //{
                //    sessions = _sessions,
                //    training = _training,
                //    trainerName = _trainerName
                //};
                //return Ok(data);
                var sessions = dbContext.Set<TrainingSession>().Where(t => t.Training_id == trainingId);
                return Ok(sessions);

            });
        }
      


        [HttpPut("create")]
        public ActionResult CreateSession(TrainingSession session)
        {
            return this.Run(() =>
            {
                var newSession = new TrainingSession
                {
                    Id = session.Id,
                    Training_id = dbContext.Set<Training>().FirstOrDefault(t => t.Id == session.Training_id).Id,
                    Location_id = dbContext.Set<Location>().FirstOrDefault(l => l.Id == session.Location_id).Id,
                    Date = session.Date,
                    Price = session.Price,
                    Minutes = session.Minutes,
                    Address_name = session.Address_name,
                    Place_name = session.Place_name,
                };
                //newSession.Training.Trainer_id = session.Training.Trainer_id;
                //newSession.Training.Category_id = session.Training.Category_id;
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