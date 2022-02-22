using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class TrainingSessionControllerTest
    {
        //TestDbContext context;
        IConfiguration config;

        public TrainingSessionControllerTest()
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
        public void ListByTraining()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);
                var result = sut.ListByTraining(3);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;

                List<TrainingSession> sessions = value.GetPropertyValue<List<TrainingSession>>("sessions");
                Assert.IsType<List<TrainingSession>>(sessions);

                string trainer = value.GetPropertyValue<string>("trainer");
                Assert.IsType<string>(trainer);   
                
                Training training = value.GetPropertyValue<Training>("training");
                Assert.IsType<Training>(training);

                Images image = value.GetPropertyValue<Images>("image");
                Assert.Null(image);

                Category category = value.GetPropertyValue<Category>("category");
                Assert.IsType<Category>(category);

                List<Tag> tags = value.GetPropertyValue<List<Tag>>("tags");
                Assert.IsType<List<Tag>>(tags);

                Assert.Equal(6, sessions[0].Id);
                Assert.Equal(3, sessions[0].Training_id);
                Assert.Equal(59, sessions[0].Location_id);
                Assert.Equal(1500, sessions[0].Price);
                Assert.Equal(45, sessions[0].Minutes);
                Assert.Equal(1, sessions[0].Min_member);
                Assert.Equal(4, sessions[0].Max_member);
                Assert.Equal(1, sessions[0].Number_of_applicants);
                Assert.Equal("Virág utca 8.", sessions[0].Address_name);
                Assert.Equal("Sportközpont", sessions[0].Place_name);
                Assert.Equal("2022.03.12 12:30:00", sessions[0].Date.ToString());

                Assert.Equal("Edzõ Béla", trainer);

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.IndexImageId);
                Assert.Equal("Edzés 3", training.Name);
                Assert.Equal("Rövid leírás az edzésrõl", training.Description);
                Assert.Equal("+36701234566", training.Contact_phone);

                Assert.Equal(3, category.Id);
                Assert.Equal("Labdarúgás", category.Name);
                Assert.Equal("football.jpg", category.Img_src);

                Assert.Equal(1, tags[0].Id);
                Assert.Equal(5, tags[1].Id);
                Assert.Equal(9, tags[2].Id);

                Assert.Equal("Csoportos", tags[0].Name);
                Assert.Equal("Zsírégetõ", tags[1].Name);
                Assert.Equal("Rehabilitációs", tags[2].Name);

                Assert.Equal("#6610f2", tags[0].Colour);
                Assert.Equal("#0dcaf0", tags[1].Colour);
                Assert.Equal("#373F51", tags[2].Colour);
            }
        }

        [Fact]
        public void ListAppliedSessions()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);
                var result = sut.ListAppliedSessions(3,9);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;

                List<TrainingSession> sessions = value.GetPropertyValue<List<TrainingSession>>("sessions");
                Assert.IsType<List<TrainingSession>>(sessions);

                Training training = value.GetPropertyValue<Training>("training");
                Assert.IsType<Training>(training);

                Assert.Equal(6, sessions[0].Id);
                Assert.Equal(3, sessions[0].Training_id);
                Assert.Equal(59, sessions[0].Location_id);
                Assert.Equal(1500, sessions[0].Price);
                Assert.Equal(45, sessions[0].Minutes);
                Assert.Equal(1, sessions[0].Min_member);
                Assert.Equal(4, sessions[0].Max_member);
                Assert.Equal(1, sessions[0].Number_of_applicants);
                Assert.Equal("Virág utca 8.", sessions[0].Address_name);
                Assert.Equal("Sportközpont", sessions[0].Place_name);
                Assert.Equal("2022.03.12 12:30:00", sessions[0].Date.ToString());

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.IndexImageId);
                Assert.Equal("Edzés 3", training.Name);
                Assert.Equal("Rövid leírás az edzésrõl", training.Description);
                Assert.Equal("+36701234566", training.Contact_phone);

            }
        }

        [Fact]
        public void GetById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);
                var result = sut.GetById(6);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;

                TrainingSession session = value.GetPropertyValue<TrainingSession>("session");
                Assert.IsType<TrainingSession>(session);

                Training training = value.GetPropertyValue<Training>("training");
                Assert.IsType<Training>(training);

                Location location = value.GetPropertyValue<Location>("location");
                Assert.IsType<Location>(location);

                Assert.Equal(6, session.Id);
                Assert.Equal(3, session.Training_id);
                Assert.Equal(59, session.Location_id);
                Assert.Equal(1500, session.Price);
                Assert.Equal(45, session.Minutes);
                Assert.Equal(1, session.Min_member);
                Assert.Equal(4, session.Max_member);
                Assert.Equal(1, session.Number_of_applicants);
                Assert.Equal("Virág utca 8.", session.Address_name);
                Assert.Equal("Sportközpont", session.Place_name);
                Assert.Equal("2022.03.12 12:30:00", session.Date.ToString());

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.IndexImageId);
                Assert.Equal("Edzés 3", training.Name);
                Assert.Equal("Rövid leírás az edzésrõl", training.Description);
                Assert.Equal("+36701234566", training.Contact_phone);

                Assert.Equal(59, location.Id);
                Assert.Equal("Csorvás", location.City_name);
                Assert.Equal("Békés", location.County_name);
            }
        }

        [Fact]
        public void CreateSession()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);
                TrainingSession newSession = new TrainingSession()
                {
                    Id = 0,
                    Address_name = "Jó utca 2.",
                    Date = new DateTime(2022, 2, 22, 11, 45, 00),
                    Location_id = 38,
                    Max_member = 5,
                    Min_member = 1,
                    Minutes = 45,
                    Number_of_applicants = 0,
                    Place_name = "Sportközpont",
                    Price = 1800,
                    Training_id = 2
                };
                var result = sut.CreateSession(newSession);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;
                TrainingSession session = (TrainingSession)((OkObjectResult)result).Value;

                Assert.IsType<TrainingSession>(session);
                Assert.Equal(12, session.Id);
                Assert.Equal(2, session.Training_id);
                Assert.Equal(38, session.Location_id);
                Assert.Equal(1800, session.Price);
                Assert.Equal(45, session.Minutes);
                Assert.Equal(1, session.Min_member);
                Assert.Equal(5, session.Max_member);
                Assert.Equal(0, session.Number_of_applicants);
                Assert.Equal("Jó utca 2.", session.Address_name);
                Assert.Equal("Sportközpont", session.Place_name);
                Assert.Equal("2022.02.22 12:45:00", session.Date.ToString());
            }
        }

        //[Fact]
        //public void Register()
        //{
        //    using (var context = TestDbContext.GenerateTestDbContext())
        //    {
        //var sut = new UserController(context, config);

        //    }
        //}
    }
}
