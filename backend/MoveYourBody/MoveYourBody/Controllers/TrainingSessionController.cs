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
                var sessions = dbContext.Set<TrainingSession>().Where(t => t.Training_id == trainingId).Select(s => new //nincs include
                {
                    id = s.Id,
                    training_id = dbContext.Set<Training>().FirstOrDefault(t => t.Id == s.Training_id).Id,
                    location_id = dbContext.Set<Location>().FirstOrDefault(l => l.Id == s.Location_id).Id,
                    date = s.Date,//.ToString("yyyy.MM.dd. hh:mm"), //??
                    price = s.Price,
                    min_member = s.Min_member,
                    max_member = s.Max_member,
                    minutes = s.Minutes,
                    address_name = s.Address_name,
                    place_name = s.Place_name,
                });
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
                    Min_member = session.Min_member,
                    Max_member = session.Max_member,
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