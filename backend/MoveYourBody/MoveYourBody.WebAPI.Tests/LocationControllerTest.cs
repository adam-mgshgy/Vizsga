using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class LocationControllerTest
    {
        IConfiguration config;

        public LocationControllerTest()
        {
            //Configuration mocking: https://stackoverflow.com/questions/64794219/how-to-mock-iconfiguration-getvalue
            var inMemorySettings = new Dictionary<string, string> {
                {"TopLevelKey", "TopLevelValue"},
                {"Auth:password", "Password"}
            };
            config = new ConfigurationBuilder()
                        .AddInMemoryCollection(inMemorySettings)
                        .Build();
        }
        [Fact]
        public void List()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new LocationController(context);
                var result = sut.List();

                Assert.IsType<OkObjectResult>(result);

                Location[] value = (Location[])((OkObjectResult)result).Value;
                Assert.IsType<OkObjectResult>(result);

            }
        }
        [Fact]
        public void ListCounties()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new LocationController(context);
                var result = sut.ListCounties();

                Assert.IsType<OkObjectResult>(result);
            }
        }
        [Fact]
        public void ListByFieldId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new LocationController(context);

                var result = sut.ListByField("1");
                Assert.IsType<OkObjectResult>(result);
                Location[] value = (Location[])((OkObjectResult)result).Value;
                Location location = (Location)value[0];
                Assert.Equal(1, location.Id);
                Assert.Equal("Aba", location.City_name);
                Assert.Equal("Fejér", location.County_name);
            }
        }
        [Fact]
        public void ListByFieldCityName()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new LocationController(context);

                var result = sut.ListByField("Aba");
                Assert.IsType<OkObjectResult>(result);
                Location[] value = (Location[])((OkObjectResult)result).Value;
                Location location = (Location)value[0];
                Assert.Equal(1, location.Id);
                Assert.Equal("Aba", location.City_name);
                Assert.Equal("Fejér", location.County_name);
            }
        }
        [Fact]
        public void ListByFieldCountyName()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new LocationController(context);

                var result = sut.ListByField("Fejér");
                Assert.IsType<OkObjectResult>(result);
                Location[] value = (Location[])((OkObjectResult)result).Value;
                Location location = (Location)value[0];
                Assert.Equal(1, location.Id);
                Assert.Equal("Aba", location.City_name);
                Assert.Equal("Fejér", location.County_name);
            }
        }
    }
}
