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
        public ActionResult ListByTraining(Training training)
        {
            return this.Run(() =>
            {

                var sessions = dbContext.Set<TrainingSession>().Where(t => t.Training.Id == training.Id).Select(s => new //nincs include
                {
                    id = s.Id,
                    training = s.Training,
                    location = s.Location,
                    date = s.Date.ToString("yyyy.MM.dd. hh:mm"), //??
                    price = s.Price,
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
                    Training = dbContext.Set<Training>().FirstOrDefault(t => t.Id == session.Training.Id),
                    Location = dbContext.Set<Location>().FirstOrDefault(l => l.Id == session.Location.Id),
                    Date = session.Date,
                    Price = session.Price,
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
                dbContext.Entry(session.Training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.Entry(session.Location).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                dbContext.Remove(session);
                dbContext.SaveChanges();
                return Ok(session);
            });
        }
    }
}