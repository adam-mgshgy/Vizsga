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
        [HttpGet("list/session")]
        public ActionResult ListBySessionId([FromQuery] int trainingSessionId)
        {
            return this.Run(() =>
            {
                var applicants = dbContext.Set<Applicant>().Where(t => t.Training_session_id == trainingSessionId).ToList();
                var users = new List<User>();
                foreach (var applicant in applicants)
                {
                    var user = dbContext.Set<User>().Where(u => u.Id == applicant.User_id).First();
                    if (!users.Contains(user))
                    {
                        users.Add(user);
                    }
                }
                return Ok(new
                {
                    applicants,
                    users
                });
            });
        }
        [HttpGet("list/user")]
        public ActionResult ListByUserId([FromQuery] int userId)
        {
            return this.Run(() =>
            {
                var sessions = dbContext.Set<Applicant>().Where(u => u.User_id == userId).ToList();
                return Ok(sessions);
            });
        }

        [HttpPut("add")]
        public ActionResult AddApplicant(Applicant applicant)
        {
            return this.Run(() =>
            {
                var newApplicant = new Applicant
                {
                    Id = 0,
                    Training_session_id = applicant.Training_session_id,
                    User_id = applicant.User_id
                };
                dbContext.Set<Applicant>().Add(newApplicant);
                dbContext.SaveChanges();
                dbContext.Set<TrainingSession>().Where(s => s.Id == newApplicant.Training_session_id).First().Number_of_applicants++;
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
                dbContext.Set<TrainingSession>().Where(s => s.Id == newApplicant.Training_session_id).First().Number_of_applicants--;
                dbContext.SaveChanges();
                //dbContext.Entry(applicant).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return Ok(newApplicant);
            });
        }
        [HttpDelete("delete")]
        public ActionResult DeleteByIds([FromQuery] int userId, [FromQuery] int sessionId)
        {
            return this.Run(() =>
            {
                var newApplicant = dbContext.Set<Applicant>().FirstOrDefault(a => a.User_id == userId && a.Training_session_id == sessionId);
                dbContext.Remove(newApplicant);
                dbContext.Set<TrainingSession>().Where(s => s.Id == newApplicant.Training_session_id).First().Number_of_applicants--;
                dbContext.SaveChanges();
                //dbContext.Entry(applicant).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return Ok(newApplicant);
            });
        }
    }
}