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
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]                                     // http://localhost:5000/categories
        public ActionResult Get()
        {
            return this.Run(() =>
            {
                var categories = dbContext.Set<Category>().ToList();
                var images = new List<Images>();
                foreach (var item in categories)
                {
                    images.AddRange(dbContext.Set<Images>().Where(i => i.Id == item.ImageId).ToList());

                }
                return Ok(new
                {
                    categories,
                    images
                });
            });
        }
        [HttpPut("add"), Authorize(Roles = "Admin")]
        public ActionResult AddCategory(Category category)
        {
            return this.Run(() =>
            {
                var newcategory = new Category
                {
                    Id = 0,
                    Name = category.Name,
                    ImageId = category.ImageId
                };
                dbContext.Set<Category>().Add(newcategory);
                dbContext.SaveChanges();
                return Ok(newcategory);
            });
        }
       
    }
}