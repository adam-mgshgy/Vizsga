using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class UserControllerTest
    {
        //TestDbContext context;
        IConfiguration config;

        public UserControllerTest()
        {
            //this.context = new TestDbContext();
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
        public void Test1()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                var result = sut.GetById(1);

                Assert.IsType<OkObjectResult>(result);

                User value = (User)((OkObjectResult)result).Value;
//                new User() { Email = "jozsiedzo@email.com", Full_name = "Edzõ József", Id = 1, Location_id = 58, Password = "jozsi", Phone_number = "+36701234567", Role = "Trainer" },

                Assert.Equal("jozsiedzo@email.com", value.Email);
                Assert.Equal("Edzõ József", value.Full_name);

            }
        }
    }
}
