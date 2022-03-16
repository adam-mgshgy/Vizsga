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
    public class TagTrainingControllerTest
    {
        IConfiguration config;
        public TagTrainingControllerTest()
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
        public void New()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagTrainingController(context);

                TagTraining tagTraining = new TagTraining()
                {
                    Id = 0,
                    Tag_id = 5,
                    Training_id = 4
                };
                var result = sut.New(tagTraining);
                Assert.IsType<OkObjectResult>(result);
                
                TagTraining value = (TagTraining)((OkObjectResult)result).Value;                

                Assert.Equal(15, value.Id);
                Assert.Equal(5, value.Tag_id);
                Assert.Equal(4, value.Training_id);                

            }
        }

        [Fact]
        public void ListByTag()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagTrainingController(context);
                
                var result = sut.ListByTag(2);
                Assert.IsType<OkObjectResult>(result);

                List<TagTraining> value = (List<TagTraining>)((OkObjectResult)result).Value;

                Assert.Equal(2, value[0].Id);
                Assert.Equal(2, value[0].Tag_id);
                Assert.Equal(1, value[0].Training_id);

            }
        }

        [Fact]
        public void ListByTraining()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagTrainingController(context);

                var result = sut.ListByTraining(2);
                Assert.IsType<OkObjectResult>(result);

                List<TagTraining> value = (List<TagTraining>)((OkObjectResult)result).Value;

                Assert.Equal(3, value[0].Id);
                Assert.Equal(1, value[0].Tag_id);
                Assert.Equal(2, value[0].Training_id);

            }
        }

        [Fact]
        public void Refresh()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagTrainingController(context);

                var result = sut.Refresh(2);
                Assert.IsType<OkObjectResult>(result);

                TagTraining value = (TagTraining)((OkObjectResult)result).Value;

                Assert.Equal(2, value.Id);
                Assert.Equal(2, value.Tag_id);
                Assert.Equal(1, value.Training_id);

            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TagTrainingController(context);

                TagTraining tagTraining = new TagTraining()
                {
                    Id = 6,
                    Tag_id = 5,
                    Training_id = 3,
                };
                var result = sut.Delete(tagTraining);
                Assert.IsType<OkObjectResult>(result);

                TagTraining value = (TagTraining)((OkObjectResult)result).Value;

                Assert.Equal(6, value.Id);
                Assert.Equal(5, value.Tag_id);
                Assert.Equal(3, value.Training_id);

            }
        }
    }
}
