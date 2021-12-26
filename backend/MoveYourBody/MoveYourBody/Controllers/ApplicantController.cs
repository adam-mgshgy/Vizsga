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
        public ActionResult AddApplicant([FromQuery] int trainingSessionId, int userId)
        {
            return this.Run(() =>
            {

                var newApplicant = new Applicant
                {
                    Id = 1,
                    Training_session = dbContext.Set<TrainingSession>().FirstOrDefault(s => s.Id == trainingSessionId),
                    User = dbContext.Set<User>().FirstOrDefault(u => u.Id == userId)
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
        public ActionResult Delete([FromQuery] int applicantId)
        {
            return this.Run(() =>
            {
                var newApplicant = dbContext.Set<Applicant>().FirstOrDefault(a => a.Id == applicantId);
                dbContext.Remove(newApplicant);
                dbContext.SaveChanges();
                //dbContext.Entry(applicant).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return Ok(newApplicant);
            });
        }
    }
}