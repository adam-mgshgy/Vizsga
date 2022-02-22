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
    public class TrainingControllerTest
    {
        IConfiguration config;
        public TrainingControllerTest()
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
        public void GetById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);
                var result = sut.GetById(2);
                //new Training() { Id = 2, Category_id = 2, Contact_phone = "+36701234567", Description = "Rövid leírás az edzésről", Name = "Edzés 2", Trainer_id = 1 },

                Assert.IsType<OkObjectResult>(result);
                Training value = (Training)((OkObjectResult)result).Value;
                Assert.Equal(2, value.Id);
                Assert.Equal(2, value.Category_id);
                Assert.Equal("+36701234567", value.Contact_phone);
                Assert.Equal("Rövid leírás az edzésről", value.Description);
                Assert.Equal("Edzés 2", value.Name);
                Assert.Equal(1, value.Trainer_id);
            }
        }

        [Fact]
        public void New()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);
                Training training = new Training()
                {
                    Id = 0,
                    Category_id = 5,
                    Trainer_id = 3,
                    Name = "Edzés",
                    Description = "Futó edzés kicsiknek és nagyoknak",
                    Contact_phone = "+36301234567",
                    IndexImageId = 0
                };
                var result = sut.New(training);
                Assert.IsType<OkObjectResult>(result);

                Training value = (Training)((OkObjectResult)result).Value;
                Assert.Equal(8, value.Id);
                Assert.Equal(5, value.Category_id);
                Assert.Equal(3, value.Trainer_id);
                Assert.Equal("Edzés", value.Name);
                Assert.Equal("Futó edzés kicsiknek és nagyoknak", value.Description);
                Assert.Equal("+36301234567", value.Contact_phone);
                Assert.Equal(0, value.IndexImageId);
            }
        }

        [Fact]
        public void Modify()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //new Training() { Id = 5, Category_id = 5, Contact_phone = "+36701234565", Description = "Rövid leírás az edzésről", Name = "Edzés 5", Trainer_id = 3 },

                var sut = new TrainingController(context);
                Training modifyTraining = new Training()
                {
                    Id = 5,
                    Category_id = 5,
                    Trainer_id = 3,
                    Name = "Edzés új neve",
                    Description = "Rövid megváltoztatott leírás az edzésről",
                    Contact_phone = "+36301234567",
                    IndexImageId = 2
                };
                var result = sut.Modify(modifyTraining);
                Assert.IsType<OkObjectResult>(result);

                Training value = (Training)((OkObjectResult)result).Value;
                
                Assert.Equal(5, value.Id);
                Assert.Equal(5, value.Category_id);
                Assert.Equal(3, value.Trainer_id);
                Assert.Equal("Edzés új neve", value.Name);
                Assert.Equal("Rövid megváltoztatott leírás az edzésről", value.Description);
                Assert.Equal("+36301234567", value.Contact_phone);
                Assert.Equal(2, value.IndexImageId);
            }
        }

        [Fact]
        public void SaveImages()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);
                string[] base64 = {"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==" };
                var result = sut.SaveImages(base64, 1);
                Assert.IsType<OkObjectResult>(result);

            }
        }
    }
}
