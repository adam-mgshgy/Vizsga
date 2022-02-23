using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;
using Microsoft.AspNetCore.Authorization;


namespace MoveYourBody.WebAPI.Controllers
{
    [Route("tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TagController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult ListAll()
        {
            return this.Run(() =>
            {
                var tag = dbContext.Set<Tag>().ToList();
                return Ok(tag);
            });
        }
        [HttpPut("add"), Authorize(Roles = "Admin")]
        public ActionResult AddTag(Tag tag)
        {
            return this.Run(() =>
            {
                var newTag = new Tag
                {
                    Id = 0,
                    Name = tag.Name,
                    Colour = tag.Colour
                };
                dbContext.Set<Tag>().Add(newTag);
                dbContext.SaveChanges();
                return Ok(newTag);
            });
        }
    }
}