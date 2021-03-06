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
    public class TrainingSessionControllerTest
    {
        IConfiguration config;

        public TrainingSessionControllerTest()
        {
            //Configuration mocking: https://stackoverflow.com/questions/64794219/how-to-mock-iconfiguration-getvalue
            byte[] data = Convert.FromBase64String("TTB2M3kwdXJiMGR5");
            string decodedString = Encoding.UTF8.GetString(data);
            var inMemorySettings = new Dictionary<string, string> {
                {"TopLevelKey", "TopLevelValue"},
                {"Auth:password",  decodedString}
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

                Image image = value.GetPropertyValue<Image>("image");
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
                Assert.Equal("Vir?g utca 8.", sessions[0].Address_name);
                Assert.Equal("Sportk?zpont", sessions[0].Place_name);
                Assert.Equal("2022. 03. 30. 12:30:00", sessions[0].Date.ToString());

                Assert.Equal("Edz? B?la", trainer);

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.Index_image_id);
                Assert.Equal("Fociedz?s", training.Name);
                Assert.Equal("Fociedz?s B?l?val, gyertek sokan", training.Description);
                Assert.Equal("+36701234566", training.Contact_phone);

                Assert.Equal(3, category.Id);
                Assert.Equal("Labdar?g?s", category.Name);
                Assert.Equal(3, category.Image_id);

                Assert.Equal(1, tags[0].Id);
                Assert.Equal(5, tags[1].Id);
                Assert.Equal(9, tags[2].Id);

                Assert.Equal("Csoportos", tags[0].Name);
                Assert.Equal("Zs?r?get?", tags[1].Name);
                Assert.Equal("Rehabilit?ci?s", tags[2].Name);

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
                Assert.Equal("Vir?g utca 8.", sessions[0].Address_name);
                Assert.Equal("Sportk?zpont", sessions[0].Place_name);
                Assert.Equal("2022. 03. 30. 12:30:00", sessions[0].Date.ToString());

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.Index_image_id);
                Assert.Equal("Fociedz?s", training.Name);
                Assert.Equal("Fociedz?s B?l?val, gyertek sokan", training.Description);
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
                Assert.Equal("Vir?g utca 8.", session.Address_name);
                Assert.Equal("Sportk?zpont", session.Place_name);
                Assert.Equal("2022. 03. 30. 12:30:00", session.Date.ToString());

                Assert.Equal(3, training.Id);
                Assert.Equal(3, training.Category_id);
                Assert.Equal(2, training.Trainer_id);
                Assert.Equal(0, training.Index_image_id);
                Assert.Equal("Fociedz?s", training.Name);
                Assert.Equal("Fociedz?s B?l?val, gyertek sokan", training.Description);
                Assert.Equal("+36701234566", training.Contact_phone);

                Assert.Equal(59, location.Id);
                Assert.Equal("Csorv?s", location.City_name);
                Assert.Equal("B?k?s", location.County_name);
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
                    Address_name = "J? utca 2.",
                    Date = new DateTime(2022, 2, 22, 11, 45, 00),
                    Location_id = 38,
                    Max_member = 5,
                    Min_member = 1,
                    Minutes = 45,
                    Number_of_applicants = 0,
                    Place_name = "Sportk?zpont",
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
                Assert.Equal("J? utca 2.", session.Address_name);
                Assert.Equal("Sportk?zpont", session.Place_name);
                Assert.Equal("2022. 02. 22. 12:45:00", session.Date.ToString());
            }
        }

        [Fact]
        public void Modify()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);

                TrainingSession modifySession = new TrainingSession()
                {
                    Id = 11,
                    Address_name = "Vir?g utca 11.",
                    Date = new DateTime(2022, 3, 10, 12, 0, 0),
                    Location_id = 61,
                    Max_member = 8,
                    Min_member = 5,
                    Minutes = 45,
                    Number_of_applicants = 1,
                    Place_name = "Sportk?zpont",
                    Price = 1800,
                    Training_id = 7
                };
                var result = sut.Modify(modifySession);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;
                TrainingSession session = (TrainingSession)((OkObjectResult)result).Value;

                Assert.IsType<TrainingSession>(session);
                Assert.Equal(11, session.Id);
                Assert.Equal(7, session.Training_id);
                Assert.Equal(61, session.Location_id);
                Assert.Equal(1800, session.Price);
                Assert.Equal(45, session.Minutes);
                Assert.Equal(5, session.Min_member);
                Assert.Equal(8, session.Max_member);
                Assert.Equal(1, session.Number_of_applicants);
                Assert.Equal("Vir?g utca 11.", session.Address_name);
                Assert.Equal("Sportk?zpont", session.Place_name);
                Assert.Equal("2022. 03. 10. 12:00:00", session.Date.ToString());
            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingSessionController(context, config);

                TrainingSession deleteSession = new TrainingSession()
                {
                    Id = 11,
                    Address_name = "Vir?g utca 8.",
                    Date = new DateTime(2022, 5, 10, 12, 0, 0),
                    Location_id = 61,
                    Max_member = 10,
                    Min_member = 5,
                    Minutes = 45,
                    Number_of_applicants = 1,
                    Place_name = "Sportk?zpont",
                    Price = 1500,
                    Training_id = 7
                };
                var result = sut.Delete(deleteSession);
                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;
                TrainingSession session = (TrainingSession)((OkObjectResult)result).Value;

                Assert.IsType<TrainingSession>(session);
                Assert.Equal(11, session.Id);
                Assert.Equal(7, session.Training_id);
                Assert.Equal(61, session.Location_id);
                Assert.Equal(1500, session.Price);
                Assert.Equal(45, session.Minutes);
                Assert.Equal(5, session.Min_member);
                Assert.Equal(10, session.Max_member);
                Assert.Equal(1, session.Number_of_applicants);
                Assert.Equal("Vir?g utca 8.", session.Address_name);
                Assert.Equal("Sportk?zpont", session.Place_name);
                Assert.Equal("2022. 05. 10. 12:00:00", session.Date.ToString());
            }
        }
    }
}
