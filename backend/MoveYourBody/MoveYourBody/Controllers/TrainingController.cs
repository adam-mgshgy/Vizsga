using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("training")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TrainingController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPut]
        public ActionResult New(Training training)
        {
            return this.Run(() =>
            {
                Training newTraining = new Training()
                {
                    Id = training.Id,
                    Name = training.Name,
                    Category_id = training.Category_id,
                    Trainer_id = training.Trainer_id,
                    Description = training.Description,
                    Contact_phone = training.Contact_phone,
                };
                dbContext.Set<Training>().Add(newTraining);
                dbContext.SaveChanges();
                return Ok(newTraining);

            });
        }
        [HttpPost("modify")]
        public ActionResult Modify(Training training)
        {
            return this.Run(() =>
            {                
                dbContext.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;                
                dbContext.SaveChanges();
                return Ok(training);
            });
        }
        [HttpDelete]
        public ActionResult Delete(Training training)
        {
            return this.Run(() =>
            {
                dbContext.Remove(training);
                dbContext.SaveChanges();
                return Ok(training);
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return this.Run(() =>
            {
                var training = dbContext.Set<Training>().Where(t => t.Id == id).FirstOrDefault();
                if (training == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező edzés"
                    });
                return Ok(training);
            });
        }
        [HttpGet("data")]
        public ActionResult ListDataById([FromQuery] int trainingId)
        {
            return this.Run(() =>
            {
                Training training = dbContext.Set<Training>().Where(t => t.Id == trainingId).FirstOrDefault();
                User trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                Location location = dbContext.Set<Location>().Where(l => l.Id == trainer.Location_id).FirstOrDefault();
                return Ok(new
                {
                    trainer,
                    training,
                    location
                });
            });
        }


        [HttpGet("TrainerId/{trainerId}")]
        public ActionResult GetByTrainerId(int trainerId)
        {
            return this.Run(() =>
            {
                var trainer = dbContext.Set<User>().Where(u => u.Id == trainerId).FirstOrDefault();
                var trainings = dbContext.Set<Training>().Where(t => t.Trainer_id == trainerId).ToList();
                if (trainings == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező edzés ehhez a felhasználóhoz"
                    });
                var tagTrainings = new List<TagTraining>();

                foreach (var training in trainings)
                {
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());

                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainer
                });
            });
        }
        [HttpGet("UserId/{userId}")]
        public ActionResult GetByUserId(int userId)
        {
            return this.Run(() =>
            {
                var applications = dbContext.Set<Applicant>().Where(a => a.User_id == userId).ToList();
                var sessions = new List<TrainingSession>();
                foreach (var application in applications)
                {
                    var sess = dbContext.Set<TrainingSession>().Where(s => s.Id == application.Training_session_id).FirstOrDefault();
                    if (sess != null)
                    {
                        sessions.Add(sess);
                    }
                }
                var trainings = new List<Training>();
                foreach (var session in sessions)
                {
                    var training = dbContext.Set<Training>().Where(t => t.Id == session.Training_id).FirstOrDefault();
                    if (!trainings.Contains(training))
                        trainings.Add(training);
                }
                var trainers = new List<User>();
                var tagTrainingList = new List<TagTraining>();
                foreach (var training in trainings)
                {
                    var tagTraining = dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList();
                    foreach (var tag in tagTraining)
                    {
                        tagTrainingList.Add(tag);
                    }
                    trainers.Add(dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault());
                }
                return Ok(new
                {
                    trainings,
                    tagTrainingList,
                    trainers,
                    sessions,
                    applications
                });
            });
        }
        [HttpGet("category")]
        public ActionResult GetByCategory([FromQuery] int id)
        {
            return this.Run(() =>
            {
                var trainings = dbContext.Set<Training>().Where(t => t.Category_id == id).ToList();
                if (trainings == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Ezzel a kategóriával nincs edzés létrehozva"
                    });
                var trainers = new List<User>();
                var tagTrainings = new List<TagTraining>();
                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }
        [HttpGet("tag")]
        public ActionResult GetByTag([FromQuery] int id)
        {
            return this.Run(() =>
            {
                var tagTrainings = dbContext.Set<TagTraining>().Where(t => t.Tag_id == id).ToList();
                var trainings = new List<Training>();
                foreach (var training in tagTrainings)
                {
                    trainings.AddRange(dbContext.Set<Training>().Where(t => t.Id == training.Training_id));
                }
                var trainers = new List<User>();
                tagTrainings = new List<TagTraining>();
                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }

        [HttpGet("all")]
        public ActionResult GetAll()
        {
            return this.Run(() =>
            {
                var trainings = dbContext.Set<Training>().ToList();
                var trainers = new List<User>();
                var tagTrainings = new List<TagTraining>();

                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());

                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }
        // [HttpGet("location")]
        // public ActionResult GetByLocation([FromQuery] string field)
        // {
        //     return this.Run(() =>
        //     {
        //         int.TryParse(field, out int id);
        //         var training = dbContext.Set<Training>()
        //         .Include(u => u.Trainer.Location)
        //         .Where(c => 
        //             c.Trainer.Location.County_name == field || 
        //             c.Trainer.Location.City_name == field ||
        //             c.Trainer.Location.Id == id)
        //         .Select(t => new

        //         {
        //             Id = t.Id,
        //             Name = t.Name,
        //             Trainer = dbContext.Set<User>().Any(u => u.Id == t.Trainer.Id),
        //             Category = t.Category.Name,
        //             Description = t.Description,
        //             Contact_phone = t.Contact_phone,
        //             Location = dbContext.Set<Location>().FirstOrDefault(l => l.City_name == t.Trainer.Location.City_name)
        //         });
        //         return Ok(training);
        //     });

        // }
    }
}