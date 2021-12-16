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
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("{id}")]                                     // http://localhost:5000/user/12
        public ActionResult Get(int id)
        {
            return this.Run(() =>
            {
                var user = dbContext.Set<User>()
                                            .Include(u => u.Location)
                                            .Where(u => u.Id == id)
                                            .Select(u => new
                                            {
                                                id = u.Id,
                                                email = u.Email,
                                                password = u.Password,
                                                phone_number = u.Phone_number,
                                                trainer = u.Trainer,
                                                location = u.Location
                                            })
                                            .FirstOrDefault();

                if (user == null)
                    return BadRequest(new
                    {
                        ErrorMessage = "Nem létező felhasználó"
                    });
                return Ok(user);
            });
        }
    }
}