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
    [Route("tagTraining")]
    [ApiController]
    public class TagTrainingController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TagTrainingController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut]
        public ActionResult New(TagTraining tagTraining)
        {
            return this.Run(() =>
            {
                if (dbContext.Set<TagTraining>().Any(t => t.Tag.Id == tagTraining.Tag.Id && t.Training.Id == tagTraining.Training.Id))
                    return BadRequest(new
                    {
                        ErrorMessage = "A megadott edzés-tag kombináció már létezik"
                    });
                var newTagTraining = new TagTraining()
                {
                    Id = tagTraining.Id,
                    Tag = dbContext.Set<Tag>().FirstOrDefault(t => t.Name == tagTraining.Tag.Name),
                    Training = dbContext.Set<Training>().FirstOrDefault(t => t.Name == tagTraining.Training.Name),
                };


                dbContext.Set<TagTraining>().Add(newTagTraining);
                dbContext.SaveChanges();
                return Ok(newTagTraining);
            });
        }

        [HttpGet("tag")]
        public ActionResult ListByTag([FromQuery] string field)
        {
            return this.Run(() =>
            {                
                int.TryParse(field, out int id);                
                
                var tagTraining = dbContext.Set<TagTraining>().Where(t => t.Tag.Id == id || t.Tag.Name == field).Select(t => new
                {
                    Id = t.Id,
                    Training = dbContext.Set<Training>().FirstOrDefault(tr => tr.Id == t.Training.Id),
                    Tag = dbContext.Set<Tag>().FirstOrDefault(tag => tag.Id == t.Tag.Id)
                });
                return Ok(tagTraining);
            });
        }
        [HttpGet("training")]
        public ActionResult ListByTraining([FromQuery] int id)
        {
            return this.Run(() =>
            {               
                var tagTraining = dbContext.Set<TagTraining>().Where(t => t.Training.Id == id).Select(t => new
                {
                    Id = t.Id,
                    Training = dbContext.Set<Training>().FirstOrDefault(tr => tr.Id == t.Training.Id),
                    Tag = dbContext.Set<Tag>().FirstOrDefault(tag => tag.Id == t.Tag.Id)
                });
                return Ok(tagTraining);
            });
        }

        [HttpDelete]
        public ActionResult Delete(TagTraining tagTraining)
        {
            return this.Run(() =>
            {

                dbContext.Remove(tagTraining);
                dbContext.SaveChanges();
                return Ok(tagTraining);
            });
        }

    }
}