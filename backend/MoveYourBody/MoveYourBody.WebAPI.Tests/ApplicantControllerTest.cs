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
        IConfiguration config;

        public ApplicantControllerTest()
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
        public void ListBySessionId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                var result = sut.ListBySessionId(7);

                Assert.IsType<OkObjectResult>(result);

                var value = ((OkObjectResult)result).Value;

                List<Applicant> applicants = value.GetPropertyValue<List<Applicant>>("applicants");
                List<User> users = value.GetPropertyValue<List<User>>("users");
                Assert.IsType<List<Applicant>>(applicants);
                Assert.IsType<List<User>>(users);
                Assert.Equal(8, applicants[0].Id);
                Assert.Equal(16, applicants[1].Id);
                Assert.Equal(7, applicants[0].Training_session_id);
                Assert.Equal(7, applicants[1].Training_session_id);
                Assert.Equal(7, applicants[0].User_id);
                Assert.Equal(9, applicants[1].User_id);

                Assert.Equal(7, users[0].Id);
                Assert.Equal(9, users[1].Id);
                Assert.Equal(64, users[0].Location_id);
                Assert.Equal(66, users[1].Location_id);
                Assert.Equal(0, users[0].Image_id);
                Assert.Equal(0, users[1].Image_id);
                Assert.Equal("+36701234561", users[0].Phone_number);
                Assert.Equal("+36701234568", users[1].Phone_number);
                Assert.Equal("User", users[0].Role);
                Assert.Equal("User", users[0].Role);
                Assert.Equal("User Éva", users[0].Full_name);
                Assert.Equal("User Gabriella", users[1].Full_name);
                Assert.Equal("evi@email.com", users[0].Email);
                Assert.Equal("gabi@email.com", users[1].Email);

            }
        }
        [Fact]
        public void ListByUserId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new ApplicantController(context);
                var result = sut.ListByUserId(4);
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

                var result = sut.DeleteByIds(4, 1);
                Assert.IsType<OkObjectResult>(result);

                Applicant value = (Applicant)((OkObjectResult)result).Value;
                Assert.Equal(1, value.Id);
                Assert.Equal(1, value.Training_session_id);
                Assert.Equal(4, value.User_id);

            }
        }
    }
}
