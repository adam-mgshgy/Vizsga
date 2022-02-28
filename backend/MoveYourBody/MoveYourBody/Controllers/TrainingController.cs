using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPut("Images"), Authorize(Roles = "Trainer, Admin")]
        public ActionResult SaveImages(string[] images, [FromQuery] int trainingId)
        {
            return this.Run(() =>
            {                
                foreach (var item in images)
                {
                    byte[] image = Convert.FromBase64String(item.Split(',')[1]);
                    Images newImage = new Images()
                    {
                        Id = 0,
                        ImageData = image,
                    };
                    dbContext.Set<Images>().Add(newImage);
                    dbContext.SaveChanges();
                    int imageId = -1;
                    foreach (var dbimage in dbContext.Set<Images>())
                    {
                        if (dbimage.ImageData == image)
                        {
                            imageId = dbimage.Id;
                        }
                    }
                    TrainingImages newTrainingImages = new TrainingImages()
                    {
                        Id = 0,
                        ImageId = imageId,
                        TrainingId = trainingId,
                    };
                    dbContext.Set<TrainingImages>().Add(newTrainingImages);
                    dbContext.SaveChanges();

                    var training = dbContext.Set<Training>().Where(t => t.Id == newTrainingImages.TrainingId).FirstOrDefault();
                    if (dbContext.Set<TrainingImages>().Where(t => t.TrainingId == training.Id).Count() == 1)
                    {
                        training.IndexImageId = dbContext.Set<Images>().Where(t => t.ImageData == image).FirstOrDefault().Id;
                        dbContext.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    }
                    dbContext.SaveChanges();

                }

                return Ok();

            });
        }
        [HttpGet("Images/{id}"), Authorize(Roles = "Trainer, Admin, User")]
        public ActionResult GetImageById(int id)
        {
            return this.Run(() =>
            {
                var trainingImages = dbContext.Set<TrainingImages>().Where(t => t.TrainingId == id).ToList();

                List<int> lista = new List<int>();
                foreach (var item in trainingImages)
                {
                    lista.Add(item.ImageId);
                }
                var images = dbContext.Set<Images>().Where(t => t.Id == -1).ToList();

                foreach (var item in lista)
                {
                    images.Add(dbContext.Set<Images>().Where(t => t.Id == item).FirstOrDefault());
                }

                return Ok(new {
                    trainingImages,
                    images
                });
            });
        }
        [HttpPut, Authorize(Roles = "Trainer, Admin")]
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
                    IndexImageId = 0
                };

                dbContext.Set<Training>().Add(newTraining);
                dbContext.SaveChanges();               

                return Ok(newTraining);

            });
        }
        [HttpPost("modify"), Authorize(Roles = "Trainer, Admin")]
        public ActionResult Modify(Training training)
        {
            return this.Run(() =>
            {                
                dbContext.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;                
                dbContext.SaveChanges();
                return Ok(training);
            });
        }
        [HttpDelete, Authorize(Roles = "Trainer, Admin")]
        public ActionResult Delete(Training training)
        {
            return this.Run(() =>
            {
                dbContext.Remove(training);
                dbContext.SaveChanges();
                return Ok(training);
            });
        }
        [HttpDelete("Images/delete"), Authorize(Roles = "Trainer, Admin")]
        public ActionResult DeleteImage(int[] id)
        {
            return this.Run(() =>
            {
                foreach (var item in id)
                {
                    var trainingImages = dbContext.Set<TrainingImages>().Where(t => t.ImageId == item).FirstOrDefault();
                    dbContext.Remove(trainingImages);
                    var image = dbContext.Set<Images>().Where(i => i.Id == trainingImages.ImageId).FirstOrDefault();
                    dbContext.Remove(image);
                    dbContext.SaveChanges();

                    var training = dbContext.Set<Training>().Where(t => t.Id == trainingImages.TrainingId).FirstOrDefault();
                    if (training.IndexImageId == item && dbContext.Set<TrainingImages>().Where(t => t.TrainingId == trainingImages.TrainingId) != null)
                    {
                        training.IndexImageId = dbContext.Set<TrainingImages>().Where(t => t.TrainingId == trainingImages.TrainingId).FirstOrDefault().ImageId;
                    }
                    else if (dbContext.Set<TrainingImages>().Where(t => t.TrainingId == trainingImages.TrainingId) == null)
                    {
                        training.IndexImageId = 0;
                    }
                    dbContext.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
                return Ok();
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return this.Run(() =>
            {

                var training = dbContext.Set<Training>()
                                            .Where(t => t.Id == id)                                            
                                            .FirstOrDefault();

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
                trainer.PasswordHash = null;
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
                trainer.PasswordHash = null;
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
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
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
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                Category category = dbContext.Set<Category>().Where(c => c.Id == id).FirstOrDefault();
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers,
                    category
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
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                Tag tag = dbContext.Set<Tag>().Where(t => t.Id == id).FirstOrDefault();

                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers,
                    tag
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
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }

        [HttpGet("county")]
        public ActionResult getByCounty([FromQuery] string county)
        {
            return this.Run(() =>
            {
                var trainings = new List<Training>();
                var trainers = new List<User>();

                    var locIds = dbContext.Set<Location>().Where(l => l.County_name == county).ToList();
                    var sessionIds = new List<TrainingSession>();
                    foreach (var locId in locIds)
                    {
                        sessionIds.AddRange(dbContext.Set<TrainingSession>().Where(s => s.Location_id == locId.Id).ToList());
                    }
                    var trainingsBySessionId = new List<Training>();
                    foreach (var session in sessionIds)
                    {
                        var trainingsBySession = dbContext.Set<Training>().Where(t => t.Id == session.Training_id).ToList();
                        foreach (var tr in trainingsBySession)
                        {
                            if (!trainings.Contains(tr))
                            {
                                trainings.Add(tr);
                            }
                        }
                    }

                var tagTrainings = new List<TagTraining>();

                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());

                }
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }
        [HttpGet("city")]
        public ActionResult getByCity([FromQuery] string city)
        {
            return this.Run(() =>
            {
                var trainings = new List<Training>();
                var trainers = new List<User>();
                
                    var locIds = dbContext.Set<Location>().Where(l => l.City_name == city).ToList();
                    var sessionIds = new List<TrainingSession>();
                    foreach (var locId in locIds)
                    {
                        sessionIds.AddRange(dbContext.Set<TrainingSession>().Where(s => s.Location_id == locId.Id).ToList());
                    }
                    var trainingsBySessionId = new List<Training>();
                    foreach (var session in sessionIds)
                    {
                        var trainingsBySession = dbContext.Set<Training>().Where(t => t.Id == session.Training_id).ToList();
                        foreach (var tr in trainingsBySession)
                        {
                            if (!trainings.Contains(tr))
                            {
                                trainings.Add(tr);
                            }
                        }
                    }
               
                var tagTrainings = new List<TagTraining>();

                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());

                }
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }
        [HttpGet("name")]
        public ActionResult GetByName([FromQuery] string trainingName)
        {
            return this.Run(() =>
            {
                //add trainings to list that match the name
                var trainings = new List<Training>();
                var trainers = new List<User>();

                if (trainingName != null)
                {
                    var trainingsByName = dbContext.Set<Training>().Where(t => t.Name.ToLower().Contains(trainingName.ToLower())).ToList();
                    trainings.AddRange(trainingsByName);
                }
                var tagTrainings = new List<TagTraining>();

                foreach (var training in trainings)
                {
                    var trainer = dbContext.Set<User>().Where(u => u.Id == training.Trainer_id).FirstOrDefault();
                    if (!trainers.Contains(trainer)) trainers.Add(trainer);
                    tagTrainings.AddRange(dbContext.Set<TagTraining>().Where(t => t.Training_id == training.Id).ToList());

                }
                foreach (var item in trainers)
                {
                    item.PasswordHash = null;
                }
                return Ok(new
                {
                    trainings,
                    tagTrainings,
                    trainers
                });
            });
        }
    }
}