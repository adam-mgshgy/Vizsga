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
                if (dbContext.Set<TagTraining>().Any(t => t.Tag_id == tagTraining.Tag_id && t.Training_id == tagTraining.Training_id))
                    return BadRequest(new
                    {
                        ErrorMessage = "A megadott edzés-tag kombináció már létezik"
                    });
                var newTagTraining = new TagTraining()
                {
                    Id = tagTraining.Id,
                    Tag_id = tagTraining.Tag_id,
                    Training_id =  tagTraining.Training_id,
                };


                dbContext.Set<TagTraining>().Add(newTagTraining);
                dbContext.SaveChanges();
                return Ok(newTagTraining);
            });
        }

        [HttpGet("tag")]
        public ActionResult ListByTag([FromQuery] int id)
        {
            return this.Run(() =>
            {                                                               
                var tagTraining = dbContext.Set<TagTraining>().Where(t => t.Tag_id == id).Select(t => new
                {
                    Id = t.Id,
                    Training_id = t.Training_id,
                    Tag_id = t.Tag_id
                });
                return Ok(tagTraining);
            });
        }
        [HttpGet("training")]
        public ActionResult ListByTraining([FromQuery] int id)
        {
            return this.Run(() =>
            {               
                var tagTraining = dbContext.Set<TagTraining>().Where(t => t.Training_id == id).Select(t => new
                {
                    Id = t.Id,
                    Training_id = t.Training_id,
                    Tag_id = t.Tag_id
                });
                return Ok(tagTraining);
            });
        }
       
        [HttpGet("refresh")]
        public ActionResult Refresh([FromQuery] int id)
        {
            return this.Run(() =>
            {
                var delete = dbContext.Set<TagTraining>().Where(d => d.Id == id).Select(t => new
                {
                    Id = t.Id,
                    Training_id = t.Training_id,
                    Tag_id = t.Tag_id
                }).FirstOrDefault();

                dbContext.Remove(delete);
                dbContext.SaveChanges();

                
                return Ok(delete);
            });
        }

        [HttpGet("GetTags")]
        public ActionResult GetTags(int id)
        {
            return this.Run(() =>
            {
                var training = dbContext.Set<Training>()
                                            .Where(t => t.Category_id == id)
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
                                            });

                var tagTraining = ListByTraining(0);
                foreach (var item in training)
                {                    
                    tagTraining = ListByTraining(item.Id);                        
                }

                return Ok(tagTraining);
            });
        }

        [HttpDelete]
        public ActionResult Delete(TagTraining tagTraining)
        {
            //return this.Run(() =>
            //{                
            //    dbContext.Remove(tagTraining);
            //    dbContext.SaveChanges();
            //    return Ok(tagTraining);
            //});
            return this.Run(() =>
            {
                var delete = dbContext.Set<TagTraining>().Where(d => d.Tag_id == tagTraining.Tag_id && d.Training_id == tagTraining.Training_id).FirstOrDefault();
                //return BadRequest(new
                //{                    
                //    ErrorMessage = delete
                //});

                dbContext.Remove(delete);
                dbContext.SaveChanges();

                
                return Ok(delete);
            });
        }

    }
}