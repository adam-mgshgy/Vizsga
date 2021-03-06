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

                Assert.IsType<OkObjectResult>(result);
                Training value = (Training)((OkObjectResult)result).Value;
                Assert.Equal(2, value.Id);
                Assert.Equal(2, value.Category_id);
                Assert.Equal("+36701234567", value.Contact_phone);
                Assert.Equal("Gyere Crossfit edzésre Józsi edzővel, ha szeretnél fitt lenni!", value.Description);
                Assert.Equal("Józsi Crossfit", value.Name);
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
                    Index_image_id = 0
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
                Assert.Equal(0, value.Index_image_id);
            }
        }

        [Fact]
        public void Modify()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);
                Training modifyTraining = new Training()
                {
                    Id = 5,
                    Category_id = 5,
                    Trainer_id = 3,
                    Name = "Edzés új neve",
                    Description = "Rövid megváltoztatott leírás az edzésről",
                    Contact_phone = "+36301234567",
                    Index_image_id = 2
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
                Assert.Equal(2, value.Index_image_id);
            }
        }

        [Fact]
        public void SaveImages()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);
                string[] images = new string[] { "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==" };
                var result = sut.SaveImages(images, 2);
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact]
        public void GetImageById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);
                var result = sut.GetImageById(2);

                var value = ((OkObjectResult)result).Value;

                List<TrainingImage> trainingImages = value.GetPropertyValue<List<TrainingImage>>("trainingImages");
                List<Image> images = value.GetPropertyValue<List<Image>>("images");
                Assert.IsType<List<TrainingImage>>(trainingImages);
                Assert.IsType<List<Image>>(images);

                Assert.Equal("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+P+/HgAFhAJ/wlseKgAAAABJRU5ErkJggg==", Convert.ToBase64String(images[0].Image_data));
            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new TrainingController(context);

                Training training = new Training()
                {
                    Id = 5,
                    Category_id = 5,
                    Trainer_id = 3,
                    Name = "Gézi Kézi",
                    Description = "Géza Edző kézilabda edzése",
                    Contact_phone = "+36701234565",
                    Index_image_id = 0
                };
                var result = sut.Delete(training);
                Assert.IsType<OkObjectResult>(result);

                Training value = (Training)((OkObjectResult)result).Value;
                Assert.Equal(5, value.Id);
                Assert.Equal(5, value.Category_id);
                Assert.Equal(3, value.Trainer_id);
                Assert.Equal("Gézi Kézi", value.Name);
                Assert.Equal("Géza Edző kézilabda edzése", value.Description);
                Assert.Equal("+36701234565", value.Contact_phone);
                Assert.Equal(0, value.Index_image_id);

            }
        }
        [Fact]
        public void ListDataById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);
               
                var result = sut.ListDataById(5);
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;                

                User trainer = value.GetPropertyValue<User>("trainer");
                Training training = value.GetPropertyValue<Training>("training");
                Location location = value.GetPropertyValue<Location>("location");


                Assert.IsType<User>(trainer);
                Assert.Equal(3, trainer.Id);
                Assert.Equal("gezaedzo@email.com", trainer.Email);
                Assert.Equal("Edző Géza", trainer.Full_name);
                Assert.Equal(0, trainer.Image_id);
                Assert.Equal(60, trainer.Location_id);
                Assert.Equal(null, trainer.Password);
                Assert.Equal(null, trainer.PasswordHash);
                Assert.Equal("Trainer", trainer.Role);
                Assert.Equal("+36701234565", trainer.Phone_number);


                Assert.IsType<Training>(training);
                //new User() { Email = "gezaedzo@email.com", Full_name = "Edző Géza", Id = 3, Location_id = 60, Password = "geza", Phone_number = "+36701234565", Role = "Trainer" }
                Assert.Equal(5, training.Id);
                Assert.Equal(5, training.Category_id);
                Assert.Equal(3, training.Trainer_id);
                Assert.Equal("Gézi Kézi", training.Name);
                Assert.Equal("Géza Edző kézilabda edzése", training.Description);
                Assert.Equal("+36701234565", training.Contact_phone);
                Assert.Equal(0, training.Index_image_id);


                Assert.IsType<Location>(location);
                Assert.Equal(60, location.Id);
                Assert.Equal("Csurgó", location.City_name);
                Assert.Equal("Somogy", location.County_name);

            }
        }

        [Fact]
        public void GetByTrainerId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetByTrainerId(3);
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");
                User trainer = value.GetPropertyValue<User>("trainer");


                Assert.IsType<User>(trainer);
                Assert.Equal(3, trainer.Id);
                Assert.Equal("gezaedzo@email.com", trainer.Email);
                Assert.Equal("Edző Géza", trainer.Full_name);
                Assert.Equal(0, trainer.Image_id);
                Assert.Equal(60, trainer.Location_id);
                Assert.Equal(null, trainer.Password);
                Assert.Equal(null, trainer.PasswordHash);
                Assert.Equal("Trainer", trainer.Role);
                Assert.Equal("+36701234565", trainer.Phone_number);


                Assert.IsType<List<Training>>(trainings);
                //new Training() { Id = 5, Category_id = 5, Contact_phone = "+36701234565", Description = "Rövid leírás az edzésről", Name = "Edzés 5", Trainer_id = 3 },
                Assert.Equal(5, trainings[0].Id);
                Assert.Equal(12, trainings[1].Category_id);
                Assert.Equal(3, trainings[0].Trainer_id);
                Assert.Equal("Jóga edzés", trainings[1].Name);
                Assert.Equal("Jóga Gézával", trainings[0].Description);
                Assert.Equal("+36701234565", trainings[1].Contact_phone);
                Assert.Equal(0, trainings[0].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(9, tagTrainings[0].Id);
                Assert.Equal(7, tagTrainings[1].Tag_id);
                Assert.Equal(5, tagTrainings[0].Training_id);

            }
        }

        [Fact]
        public void GetByUserId()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetByUserId(4);
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainingList");
                List<TrainingSession> sessions = value.GetPropertyValue<List<TrainingSession>>("sessions");
                List<Applicant> applications = value.GetPropertyValue<List<Applicant>>("applications");



                Assert.IsType<List<User>>(trainers);
                
                Assert.Equal(1, trainers[0].Id);
                Assert.Equal("jozsiedzo@email.com", trainers[0].Email);
                Assert.Equal("Edző József", trainers[0].Full_name);
                Assert.Equal(0, trainers[0].Image_id);
                Assert.Equal(58, trainers[0].Location_id);
                Assert.Equal(null, trainers[0].Password);
                Assert.Equal(null, trainers[0].PasswordHash);
                Assert.Equal("Trainer", trainers[0].Role);
                Assert.Equal("+36701234567", trainers[0].Phone_number);


                Assert.IsType<List<Training>>(trainings);
                
                Assert.Equal(1, trainings[0].Id);
                Assert.Equal(1, trainings[0].Category_id);
                Assert.Equal(1, trainings[0].Trainer_id);
                Assert.Equal("Box Józsival", trainings[0].Name);
                Assert.Equal("Szeretettel vár Józsi Edző a legnépszerűbb box edzésén! Kezdőket és haladókat is szívesen fogadunk. Nem kell más, csak egy törölköző, víz, és hatalmas lelkesedés!", trainings[0].Description);
                Assert.Equal("+36701234567", trainings[0].Contact_phone);
                Assert.Equal(0, trainings[0].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(1, tagTrainings[0].Id);
                Assert.Equal(1, tagTrainings[0].Tag_id);
                Assert.Equal(1, tagTrainings[0].Training_id);


                Assert.IsType<List<TrainingSession>>(sessions);

                Assert.Equal(1, sessions[0].Id);
                Assert.Equal(1, sessions[0].Training_id);
                Assert.Equal(58, sessions[0].Location_id);
                Assert.Equal(1500, sessions[0].Price);
                Assert.Equal(45, sessions[0].Minutes);
                Assert.Equal(5, sessions[0].Min_member);
                Assert.Equal(10, sessions[0].Max_member);
                Assert.Equal(5, sessions[0].Number_of_applicants);
                Assert.Equal("Virág utca 8.", sessions[0].Address_name);
                Assert.Equal("Sportközpont", sessions[0].Place_name);
                Assert.Equal("2022. 04. 10. 12:30:00", sessions[0].Date.ToString());


                Assert.IsType<List<Applicant>>(applications);

                Assert.Equal(1, applications[0].Id);
                Assert.Equal(1, applications[0].Training_session_id);
                Assert.Equal(4, applications[0].User_id);

            }
        }    

        [Fact]
        public void GetByCategory()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetByCategory(2);
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");
                Category category = value.GetPropertyValue<Category>("category");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(3, trainers[1].Id);
                Assert.Equal("gezaedzo@email.com", trainers[1].Email);
                Assert.Equal("Edző Géza", trainers[1].Full_name);
                Assert.Equal(0, trainers[1].Image_id);
                Assert.Equal(60, trainers[1].Location_id);
                Assert.Equal(null, trainers[1].Password);
                Assert.Equal(null, trainers[1].PasswordHash);
                Assert.Equal("Trainer", trainers[1].Role);
                Assert.Equal("+36701234565", trainers[1].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(2, trainings[0].Id);
                Assert.Equal(2, trainings[0].Category_id);
                Assert.Equal(1, trainings[0].Trainer_id);
                Assert.Equal("Józsi Crossfit", trainings[0].Name);
                Assert.Equal("Gyere Crossfit edzésre Józsi edzővel, ha szeretnél fitt lenni!", trainings[0].Description);
                Assert.Equal("+36701234567", trainings[0].Contact_phone);
                Assert.Equal(1, trainings[0].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(4, tagTrainings[1].Id);
                Assert.Equal(4, tagTrainings[1].Tag_id);
                Assert.Equal(2, tagTrainings[1].Training_id);

                Assert.IsType<Category>(category);
                Assert.Equal(2, category.Id);
                Assert.Equal("Crossfit", category.Name);
                Assert.Equal(2, category.Image_id);

            }
        }

        [Fact]
        public void GetByTag()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetByTag(4);
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");
                Tag tag = value.GetPropertyValue<Tag>("tag");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(2, trainers[1].Id);
                Assert.Equal("belaedzo@email.com", trainers[1].Email);
                Assert.Equal("Edző Béla", trainers[1].Full_name);
                Assert.Equal(0, trainers[1].Image_id);
                Assert.Equal(59, trainers[1].Location_id);
                Assert.Equal(null, trainers[1].Password);
                Assert.Equal(null, trainers[1].PasswordHash);
                Assert.Equal("Trainer", trainers[1].Role);
                Assert.Equal("+36701234566", trainers[1].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(2, trainings[0].Id);
                Assert.Equal(2, trainings[0].Category_id);
                Assert.Equal(1, trainings[0].Trainer_id);
                Assert.Equal("Józsi Crossfit", trainings[0].Name);
                Assert.Equal("Gyere Crossfit edzésre Józsi edzővel, ha szeretnél fitt lenni!", trainings[0].Description);
                Assert.Equal("+36701234567", trainings[0].Contact_phone);
                Assert.Equal(1, trainings[0].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(4, tagTrainings[1].Id);
                Assert.Equal(4, tagTrainings[1].Tag_id);
                Assert.Equal(2, tagTrainings[1].Training_id);

                Assert.IsType<Tag>(tag);
                Assert.Equal(4, tag.Id);
                Assert.Equal("Szabadtéri", tag.Name);
                Assert.Equal("black", tag.Colour);
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetAll();
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(2, trainers[1].Id);
                Assert.Equal("belaedzo@email.com", trainers[1].Email);
                Assert.Equal("Edző Béla", trainers[1].Full_name);
                Assert.Equal(0, trainers[1].Image_id);
                Assert.Equal(59, trainers[1].Location_id);
                Assert.Equal(null, trainers[1].Password);
                Assert.Equal(null, trainers[1].PasswordHash);
                Assert.Equal("Trainer", trainers[1].Role);
                Assert.Equal("+36701234566", trainers[1].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(2, trainings[1].Id);
                Assert.Equal(2, trainings[1].Category_id);
                Assert.Equal(1, trainings[1].Trainer_id);
                Assert.Equal("Józsi Crossfit", trainings[1].Name);
                Assert.Equal("Gyere Crossfit edzésre Józsi edzővel, ha szeretnél fitt lenni!", trainings[1].Description);
                Assert.Equal("+36701234567", trainings[1].Contact_phone);
                Assert.Equal(1, trainings[1].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(2, tagTrainings[1].Id);
                Assert.Equal(2, tagTrainings[1].Tag_id);
                Assert.Equal(1, tagTrainings[1].Training_id);

            }
        }

        [Fact]
        public void GetByCounty()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.getByCounty("Győr-Moson-Sopron");
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(2, trainers[1].Id);
                Assert.Equal("belaedzo@email.com", trainers[1].Email);
                Assert.Equal("Edző Béla", trainers[1].Full_name);
                Assert.Equal(0, trainers[1].Image_id);
                Assert.Equal(59, trainers[1].Location_id);
                Assert.Equal(null, trainers[1].Password);
                Assert.Equal(null, trainers[1].PasswordHash);
                Assert.Equal("Trainer", trainers[1].Role);
                Assert.Equal("+36701234566", trainers[1].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(4, trainings[1].Id);
                Assert.Equal(4, trainings[1].Category_id);
                Assert.Equal(2, trainings[1].Trainer_id);
                Assert.Equal("Kosár Bélával", trainings[1].Name);
                Assert.Equal("Kosarazzunk együtt!", trainings[1].Description);
                Assert.Equal("+36701234566", trainings[1].Contact_phone);
                Assert.Equal(0, trainings[1].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(2, tagTrainings[1].Id);
                Assert.Equal(2, tagTrainings[1].Tag_id);
                Assert.Equal(1, tagTrainings[1].Training_id);

            }
        }

        [Fact]
        public void GetByCity()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.getByCity("Csurgó");
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(3, trainers[1].Id);
                Assert.Equal("gezaedzo@email.com", trainers[1].Email);
                Assert.Equal("Edző Géza", trainers[1].Full_name);
                Assert.Equal(0, trainers[1].Image_id);
                Assert.Equal(60, trainers[1].Location_id);
                Assert.Equal(null, trainers[1].Password);
                Assert.Equal(null, trainers[1].PasswordHash);
                Assert.Equal("Trainer", trainers[1].Role);
                Assert.Equal("+36701234565", trainers[1].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(5, trainings[1].Id);
                Assert.Equal(5, trainings[1].Category_id);
                Assert.Equal(3, trainings[1].Trainer_id);
                Assert.Equal("Gézi Kézi", trainings[1].Name);
                Assert.Equal("Géza Edző kézilabda edzése", trainings[1].Description);
                Assert.Equal("+36701234565", trainings[1].Contact_phone);
                Assert.Equal(0, trainings[1].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(4, tagTrainings[1].Id);
                Assert.Equal(4, tagTrainings[1].Tag_id);
                Assert.Equal(2, tagTrainings[1].Training_id);

            }
        }

        [Fact]
        public void GetByName()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {

                var sut = new TrainingController(context);

                var result = sut.GetByName("Edzés 5");
                Assert.IsType<OkObjectResult>(result);
                var value = ((OkObjectResult)result).Value;

                List<User> trainers = value.GetPropertyValue<List<User>>("trainers");
                List<Training> trainings = value.GetPropertyValue<List<Training>>("trainings");
                List<TagTraining> tagTrainings = value.GetPropertyValue<List<TagTraining>>("tagTrainings");


                Assert.IsType<List<User>>(trainers);

                Assert.Equal(3, trainers[0].Id);
                Assert.Equal("gezaedzo@email.com", trainers[0].Email);
                Assert.Equal("Edző Géza", trainers[0].Full_name);
                Assert.Equal(0, trainers[0].Image_id);
                Assert.Equal(60, trainers[0].Location_id);
                Assert.Equal(null, trainers[0].Password);
                Assert.Equal(null, trainers[0].PasswordHash);
                Assert.Equal("Trainer", trainers[0].Role);
                Assert.Equal("+36701234565", trainers[0].Phone_number);


                Assert.IsType<List<Training>>(trainings);

                Assert.Equal(5, trainings[0].Id);
                Assert.Equal(5, trainings[0].Category_id);
                Assert.Equal(3, trainings[0].Trainer_id);
                Assert.Equal("Gézi Kézi", trainings[0].Name);
                Assert.Equal("Géza Edző kézilabda edzése", trainings[0].Description);
                Assert.Equal("+36701234565", trainings[0].Contact_phone);
                Assert.Equal(0, trainings[0].Index_image_id);


                Assert.IsType<List<TagTraining>>(tagTrainings);

                Assert.Equal(9, tagTrainings[0].Id);
                Assert.Equal(1, tagTrainings[0].Tag_id);
                Assert.Equal(5, tagTrainings[0].Training_id);

            }
        }
    }
}
