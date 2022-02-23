using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoveYourBody.Service.Models;
using MoveYourBody.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoveYourBody.WebAPI.Tests
{
    public class CategoriesControllerTest
    {
        //TestDbContext context;
        IConfiguration config;

        public CategoriesControllerTest()
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
        public void Get()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new CategoriesController(context);
                var result = sut.Get();

                Assert.IsType<OkObjectResult>(result);

                List<Category> value = (List<Category>)((OkObjectResult)result).Value;
                Assert.IsType<List<Category>>(value);
                Assert.Equal(12, value.Count);
                Assert.Equal(1, value[0].Id);
                Assert.Equal("Box", value[0].Name);
                Assert.Equal("box.jpg", value[0].Img_src);
            }
        }
        [Fact]
        public void AdAddCategoryd()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new CategoriesController(context);
                Category newCategory = new Category()
                {
                    Id = 0,
                    Img_src = "golf.jpg",
                    Name = "Golf"
                };
                var result = sut.AddCategory(newCategory);

                Assert.IsType<OkObjectResult>(result);

                Category value = (Category)((OkObjectResult)result).Value;
                Assert.IsType<Category>(value);
                Assert.Equal(13, value.Id);
                Assert.Equal("Golf", value.Name);
                Assert.Equal("golf.jpg", value.Img_src);
            }
        }


    }
}
