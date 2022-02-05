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
                var category = dbContext.Set<Category>().Select(c => new
                {
                    id = c.Id,
                    img_src = c.Img_src,
                    name = c.Name
                });
                return Ok(category);
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
                    Img_src = category.Img_src
                };
                dbContext.Set<Category>().Add(newcategory);
                dbContext.SaveChanges();
                return Ok(newcategory);
            });
        }
        //[HttpGet("{id}")]
        //public ActionResult GetById(int id)
        //{
        //    return this.Run(() =>
        //    {
        //        var category = dbContext.Set<Category>().Where(c => c.Id == id).FirstOrDefault();
        //        return Ok(category);
        //    });
        //}
    }
}