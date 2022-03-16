using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Auth;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class AuthControllerTest
    {
        IConfiguration config;
        public AuthControllerTest()
        {
            //Configuration mocking: https://stackoverflow.com/questions/64794219/how-to-mock-iconfiguration-getvalue
            var inMemorySettings = new Dictionary<string, string> {
                {"TopLevelKey", "TopLevelValue"},
                {"Auth:password", "Password"},
                {"JwtConfig:secret", "uqMSgheawJFWEFMFcGhkKxnEsdzPSmVVhbhitlEB" },
                {"JwtConfig:expirationInMinutes", "30" },

            };
            config = new ConfigurationBuilder()
                        .AddInMemoryCollection(inMemorySettings)
                        .Build();

        }       

        [Fact]
        public void Login()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new AuthController(config, context);

                TagTraining tagTraining = new TagTraining()
                {
                    Id = 0,
                    Tag_id = 5,
                    Training_id = 4
                };
                LoginModel loginModel = new LoginModel() 
                {
                    Email = "belaedzo@email.com",
                    Password = "bela"
                };
                var result = sut.Login(loginModel);

                Assert.IsType<OkObjectResult>(result);
                
                var value = ((OkObjectResult)result).Value;
                string token = value.GetPropertyValue<string>("token");
                int userId = value.GetPropertyValue<int>("userId");

                var jwt = new JwtService(config);
                var expectedToken = jwt.GenerateSecurityToken(loginModel.Email, "Trainer");
                
                Assert.Equal(expectedToken, token);
                Assert.Equal(2, userId);

            }
        }


        
    }
}
