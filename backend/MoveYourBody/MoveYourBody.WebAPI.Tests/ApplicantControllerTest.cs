using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class ApplicantControllerTest
    {
        //TestDbContext context;
        IConfiguration config;

        public ApplicantControllerTest()
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
        public void ListBySessionId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                var result = sut.ListBySessionId(7);
                //new TrainingSession() { Address_name = "Virág utca 10.", Date = new DateTime(2022, 3, 10, 10, 30, 0), Id = 7, Location_id = 58, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 2, Place_name = "Sportközpont", Price = 1500, Training_id = 4 },
                //new Applicant() { Id = 8, Training_session_id = 7, User_id = 7 },
                //new Applicant() { Id = 16, Training_session_id = 7, User_id = 9 },
                //new User() { Email = "evi@email.com", Full_name = "User Éva", Id = 7, Location_id = 64, Password = "evi", Phone_number = "+36701234561", Role = "User" },
                //new User() { Email = "gabi@email.com", Full_name = "User Gabriella", Id = 9, Location_id = 66, Password = "gabi", Phone_number = "+36701234568", Role = "User" },

                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;
                //List<Applicant> applicants = value; ???
                Assert.IsType<OkObjectResult>(result);

            }
        }
        [Fact]
        public void ListByUserId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                var result = sut.ListByUserId(4);
                //new Applicant() { Id = 1, Training_session_id = 1, User_id = 4 },
                //new Applicant() { Id = 2, Training_session_id = 2, User_id = 4 },
                Assert.IsType<OkObjectResult>(result);

                List<Applicant> value = (List<Applicant>)((OkObjectResult)result).Value;
                Assert.Equal(2, value.Count);
                Assert.Equal(1, value[0].Id);
                Assert.Equal(2, value[1].Id);
                Assert.Equal(1, value[0].Training_session_id);
                Assert.Equal(2, value[1].Training_session_id);
                Assert.Equal(4, value[0].User_id);
                Assert.Equal(4, value[1].User_id);
            }
        }
        [Fact]
        public void AddApplicant()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                Applicant newApplicant = new Applicant()
                {
                    Id = 0,
                    User_id = 8,
                    Training_session_id = 11,
                };
                var result = sut.AddApplicant(newApplicant);
                Assert.IsType<OkObjectResult>(result);

                Applicant value = (Applicant)((OkObjectResult)result).Value;
                Assert.Equal(18, value.Id);
                Assert.Equal(11, value.Training_session_id);
                Assert.Equal(8, value.User_id);
            }
        }
        [Fact]
        public void DeleteApplicant()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                //new Applicant() { Id = 1, Training_session_id = 1, User_id = 4 },
                //new TrainingSession() { Address_name = "Virág utca 8.", Date = new DateTime(2022, 3, 10, 12, 30, 0), Id = 1, Location_id = 58, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 5, Place_name = "Sportközpont", Price = 1500, Training_id = 1 },

                var result = sut.Delete(1);
                Assert.IsType<OkObjectResult>(result);

                Applicant value = (Applicant)((OkObjectResult)result).Value;
                Assert.Equal(1, value.Id);
                Assert.Equal(1, value.Training_session_id);
                Assert.Equal(4, value.User_id);
                
            }
        }
        [Fact]
        public void DeleteByIds()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                //new Applicant() { Id = 1, Training_session_id = 1, User_id = 4 },
                //new TrainingSession() { Address_name = "Virág utca 8.", Date = new DateTime(2022, 3, 10, 12, 30, 0), Id = 1, Location_id = 58, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 5, Place_name = "Sportközpont", Price = 1500, Training_id = 1 },

                var result = sut.DeleteByIds(4, 1);
                Assert.IsType<OkObjectResult>(result);

                Applicant value = (Applicant)((OkObjectResult)result).Value;
                Assert.Equal(1, value.Id);
                Assert.Equal(1, value.Training_session_id);
                Assert.Equal(4, value.User_id);

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
