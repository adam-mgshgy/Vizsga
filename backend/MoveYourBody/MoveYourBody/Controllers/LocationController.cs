﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoveYourBody.Service;
using MoveYourBody.Service.Models;

namespace MoveYourBody.WebAPI.Controllers
{
    [Route("locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public LocationController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult List()
        {
            return this.Run(() =>
            {
                var location = dbContext.Set<Location>().Select(l => new
                {
                    id = l.Id,
                    county_name = l.County_name,
                    city_name = l.City_name

                });
                return Ok(location);
            });
        }
        [HttpGet("counties")]
        public ActionResult ListCounties()
        {
            return this.Run(() =>
            {
                var location = dbContext.Set<Location>().GroupBy(c => new { c.County_name }).Select(l => new
                {
                    county_name = l.Key.County_name
                });
                return Ok(location);
            });
        }
        [HttpGet("{field}")]                                  
        public ActionResult ListByField(string field)
        {
                return this.Run(() =>
                {
                    int.TryParse(field, out int id);
                    var location = dbContext.Set<Location>().Where(c => c.County_name == field || c.City_name == field || c.Id == id).Select(l => new
                    {
                        id = l.Id,
                        county_name = l.County_name,
                        city_name = l.City_name
                    });
                    return Ok(location);
                });
        }
    }
    
}