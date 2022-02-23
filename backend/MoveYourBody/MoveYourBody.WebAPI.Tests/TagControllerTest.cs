using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class TagControllerTest
    {
        IConfiguration config;
        public TagControllerTest()
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
        public void GetAll()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagController(context);

                var result = sut.ListAll();
                Assert.IsType<OkObjectResult>(result);
                
                List<Tag> value = (List<Tag>)((OkObjectResult)result).Value;                

                Assert.Equal(1, value[0].Id);
                Assert.Equal("Csoportos", value[0].Name);
                Assert.Equal("#6610f2", value[0].Colour);                

            }
        }

        [Fact]
        public void AddTag()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagController(context);

                Tag tag = new Tag()
                {
                    Id = 0,
                    Colour = "red",
                    Name = "Egyéni"
                };
                var result = sut.AddTag(tag);
                Assert.IsType<OkObjectResult>(result);

                var value = (Tag)((OkObjectResult)result).Value;

                Assert.Equal(13, value.Id);
                Assert.Equal("Egyéni", value.Name);
                Assert.Equal("red", value.Colour);

            }
        }

    }
}
