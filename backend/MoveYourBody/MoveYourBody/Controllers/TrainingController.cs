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
                    Min_member = training.Min_member,
                    Max_member = training.Max_member,
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
                var training = dbContext.Set<Training>()
                                            .Where(t => t.Id == id)
                                            .Select(t => new
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Trainer_id = t.Trainer_id,
                                                Category_id = t.Category_id,
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
        public ActionResult GetByCategory([FromQuery] int id)
        {
            return this.Run(() =>
            {                
                var training = dbContext.Set<Training>()
                                            .Where(t => t.Category_id == id)
                                            .Select(t => new
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Trainer_id= t.Trainer_id,
                                                Category_id = t.Category_id,
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
        //             Min_member = t.Min_member,
        //             Max_member = t.Max_member,
        //             Description = t.Description,
        //             Contact_phone = t.Contact_phone,
        //             Location = dbContext.Set<Location>().FirstOrDefault(l => l.City_name == t.Trainer.Location.City_name)
        //         });
        //         return Ok(training);
        //     });

        // }
    }
}