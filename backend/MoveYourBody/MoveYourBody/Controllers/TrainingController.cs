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
    }
}