using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("applicants")]
    [ApiController]
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ApplicantController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("list")]
        public ActionResult ListBySession([FromQuery] int trainingSessionId)
        {
            return this.Run(() =>
            {
                var applicants = dbContext.Set<Applicant>().Where(t => t.Training_session.Id == trainingSessionId).Select(s => new //nincs include
                    {
                        user = s.User
                    });
                return Ok(applicants);
            });

        }
        [HttpPut("add")]
        public ActionResult AddApplicant(Applicant applicant)
        {
            return this.Run(() =>
            {
                var newApplicant = new Applicant
                {
                    Id = applicant.Id,
                    Training_session = dbContext.Set<TrainingSession>().FirstOrDefault(s => s.Id == applicant.Training_session.Id),
                    User = dbContext.Set<User>().FirstOrDefault(u => u.Id == applicant.User.Id)
                };
                //newApplicant.Training_session.Training.Trainer = dbContext.Set<User>().FirstOrDefault(trainer => trainer.Id == newApplicant.Training_session.Training.Trainer.Id);
                //newApplicant.Training_session.Training.Trainer.Location = dbContext.Set<Location>().FirstOrDefault(tLoc => tLoc.Id == newApplicant.Training_session.Training.Trainer.Location.Id);
                //newApplicant.Training_session.Training = dbContext.Set<Training>().FirstOrDefault(training => training.Id == newApplicant.Training_session.Training.Id);
                //newApplicant.Training_session.Location = dbContext.Set<Location>().FirstOrDefault(loc => loc.Id == newApplicant.Training_session.Location.Id);
                //newApplicant.Training_session.Training.Category = dbContext.Set<Category>().FirstOrDefault(c => c.Name == newApplicant.Training_session.Training.Category.Name);
                dbContext.Set<Applicant>().Add(newApplicant);
                dbContext.SaveChanges();
                return Ok(newApplicant);
            });
        }
        
        [HttpDelete]
        public ActionResult Delete(User user)
        {
            return this.Run(() =>
            {
                dbContext.Remove(user);
                dbContext.SaveChanges();
                return Ok(user);
            });
        }
    }
}