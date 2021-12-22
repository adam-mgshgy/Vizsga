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
                var tag = dbContext.Set<Tag>().Select(t => new
                {
                    id = t.Id,
                    name = t.Name,
                    colour = t.Colour
                });
                return Ok(tag);
            });
        }
    }
}