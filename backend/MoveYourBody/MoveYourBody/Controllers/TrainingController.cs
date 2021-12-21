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

                    Contact_phone = training.Contact_phone,
                    Description = training.Description,
                    Id = training.Id,
                    Max_member = training.Max_member,
                    Min_member = training.Min_member,
                    Name = training.Name,
                    Trainer = dbContext.Set<User>().FirstOrDefault(u => u.Id == training.Trainer.Id),
                    Category = dbContext.Set<Category>().FirstOrDefault(c => c.Name == training.Category.Name)
                };
                newTraining.Trainer.Location = dbContext.Set<Location>().FirstOrDefault(l => l.Id == training.Trainer.Location.Id);
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

                //TODO category value doesn't change
                dbContext.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.Entry(training.Category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var training = dbContext.Set<Training>()
                                            .Where(t => t.Id == id)
                                            .Select(t => new
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Trainer = dbContext.Set<User>().FirstOrDefault(u => u.Id == t.Trainer.Id),
                                                Category = dbContext.Set<Category>().FirstOrDefault(c => c.Name == t.Category.Name),
                                                Min_member = t.Min_member,
                                                Max_member = t.Max_member,
                                                Description = t.Description,
                                                Contact_phone = t.Contact_phone
                                            })
                                            .FirstOrDefault();

                if (training == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező edzés"
                    });
                return Ok(training);
            });
        }
        [HttpGet("category")]
        public ActionResult GetByCategory([FromQuery] string catName)
        {
            return this.Run(() =>
            {
                var training = dbContext.Set<Training>()
                                            .Where(t => t.Category.Name == catName)
                                            .Select(t => new
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Trainer = dbContext.Set<User>().FirstOrDefault(u => u.Id == t.Trainer.Id),
                                                Category = t.Category.Name,
                                                Min_member = t.Min_member,
                                                Max_member = t.Max_member,
                                                Description = t.Description,
                                                Contact_phone = t.Contact_phone
                                            })
                                            .FirstOrDefault();

                if (training == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Ezzel a kategóriával nincs edzés létrehozva"
                    });
                return Ok(training);
            });
        }
    }
}