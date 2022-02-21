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
        public void GetById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                var result = sut.GetById(1);

                Assert.IsType<OkObjectResult>(result);

                User value = (User)((OkObjectResult)result).Value;
                //new User() { Email = "jozsiedzo@email.com", Full_name = "Edzõ József", Id = 1, Location_id = 58, Password = "jozsi", Phone_number = "+36701234567", Role = "Trainer" },

                Assert.Equal("jozsiedzo@email.com", value.Email);
                Assert.Equal("Edzõ József", value.Full_name);
                Assert.Equal(1, value.Id);
                Assert.Equal(58, value.Location_id);
                Assert.Equal("+36701234567", value.Phone_number);
                Assert.Equal("Trainer", value.Role);
                Assert.Null(value.Password);
                Assert.Null(value.PasswordHash);

            }
        }
        [Fact]
        public void CheckEmail()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                var result = sut.CheckEmail("jozsiedzo@email.com");

                Assert.IsType<OkObjectResult>(result);
                bool value = (bool)((OkObjectResult)result).Value;

                Assert.True(value);

            }
        }

        [Fact]
        public void New()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                User user = new User() 
                {
                    Id = 0,
                    Email = "tesztelek@email.com",
                    Full_name = "Teszt Elek",
                    Location_id = 1,
                    Password = "titkos",
                    Phone_number = "+36704598715",
                    Role = "User",
                    ImageId = 0,
                    PasswordHash = ""
                };
                var result = sut.New(user);
                Assert.IsType<OkObjectResult>(result);
                User value = (User)((OkObjectResult)result).Value;
                Assert.Equal("tesztelek@email.com", value.Email);
                Assert.Equal("Teszt Elek", value.Full_name);
                Assert.Equal(11, value.Id);
                Assert.Equal(1, value.Location_id);
                Assert.Equal("+36704598715", value.Phone_number);
                Assert.Equal("User", value.Role);
                Assert.Equal("titkos", value.Password);
                Assert.Equal("7fJcZIiRtzawzbLXmNxGld4nOnuyXRp2Ya35LmhSLeRQPWINvz4NoFRcL6c2WsKo5qYvk7ZpVaTIFImoi1EIwQ==", value.PasswordHash);
            }
        }
        [Fact]
        public void GetTrainer()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                var result = sut.GetTrainer(3);
                //new User() { Email = "belaedzo@email.com", Full_name = "Edzõ Béla", Id = 2, Location_id = 59, Password = "bela", Phone_number = "+36701234566", Role = "Trainer" },
                Assert.IsType<OkObjectResult>(result);
                User value = (User)((OkObjectResult)result).Value;
                Assert.Equal("belaedzo@email.com", value.Email);
                Assert.Equal("Edzõ Béla", value.Full_name);
                Assert.Equal(2, value.Id);
                Assert.Equal(0, value.ImageId);
                Assert.Equal(59, value.Location_id);
                Assert.Equal("+36701234566", value.Phone_number);
                Assert.Equal("Trainer", value.Role);
                Assert.Null(value.Password);
                Assert.Equal("z5VEUKSD1u9KhRh5xPn1HKjG13oud+Zi1soA2OcprgNPvog05dxjMcFYC54i0YoWfG4Kf04eYgEBhR1W1DBKOw==", value.PasswordHash);

            }
        }
        [Fact]
        public void Modify()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                User modifyUser = new User()
                {
                    Id = 1,
                    Email = "jozsiedzo@email.com",
                    Full_name = "Edzõ Józsi",
                    Location_id = 1,
                    Password = "jozsi",
                    Phone_number = "+36802547156",
                    Role = "Trainer",
                    ImageId = 0
                };
                var result = sut.Modify(modifyUser);
                Assert.IsType<OkObjectResult>(result);

                User value = (User)((OkObjectResult)result).Value;

                Assert.Equal("jozsiedzo@email.com", value.Email);
                Assert.Equal("Edzõ Józsi", value.Full_name);
                Assert.Equal(1, value.Id);
                Assert.Equal(1, value.Location_id);
                Assert.Equal("+36802547156", value.Phone_number);
                Assert.Equal("Trainer", value.Role);
                Assert.Equal("",value.Password);
                Assert.Equal("",value.PasswordHash);

            }
        }
        [Fact]
        public void DeleteImage()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                var result = sut.DeleteImage(1);
                Assert.IsType<BadRequestObjectResult>(result);

            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new UserController(context, config);
                User deleteUser = new User()
                {
                    Id = 1,
                    Email = "jozsiedzo@email.com",
                    Full_name = "Edzõ Józsi",
                    Location_id = 1,
                    Password = "jozsi",
                    Phone_number = "+36802547156",
                    Role = "Trainer",
                    ImageId = 0,
                    PasswordHash = ""
                };
                var result = sut.Delete(deleteUser);
                Assert.IsType<OkObjectResult>(result);
                User value = (User)((OkObjectResult)result).Value;
                Assert.Equal("jozsiedzo@email.com", value.Email);
                Assert.Equal("Edzõ Józsi", value.Full_name);
                Assert.Equal(1, value.Id);
                Assert.Equal(1, value.Location_id);
                Assert.Equal("+36802547156", value.Phone_number);
                Assert.Equal("Trainer", value.Role);
                Assert.Equal("jozsi", value.Password);
                Assert.Equal("", value.PasswordHash);
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
