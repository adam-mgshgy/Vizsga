using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.IO;

namespace MoveYourBody.Service
{
    public class ApplicationDbContext: DbContext
    {
        private readonly string connectionString;

        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<TrainingImages> TrainingImages { get; set; }
        public DbSet<TrainingSession> TrainingSession { get; set; }
        public DbSet<TagTraining> TagTraining { get; set; }
        public DbSet<Applicant> Applicant { get; set; }

        public ApplicationDbContext()
        {
#if DEBUG        
            connectionString = "Server=localhost;Database=moveyourbody;Uid=root;Pwd=;charset=utf8";
#endif
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            //https://stackoverflow.com/questions/33127296/how-to-get-connectionstring-from-ef7-dbcontext
            var ext = options.FindExtension<MySqlOptionsExtension>();
            connectionString = ext.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Box", Img_src = "box.jpg" },
                new Category() { Id = 2, Name = "Crossfit", Img_src = "crossFitt.jpg" },
                new Category() { Id = 3, Name = "Labdarúgás", Img_src = "football.jpg" },
                new Category() { Id = 4, Name = "Kosárlabda", Img_src = "basketball.jpg" },
                new Category() { Id = 5, Name = "Kézilabda", Img_src = "handball.jpg" },
                new Category() { Id = 6, Name = "Röplabda", Img_src = "volleyball.jpg" },
                new Category() { Id = 7, Name = "Spartan", Img_src = "spartan.jpg" },
                new Category() { Id = 8, Name = "Tenisz", Img_src = "tennis.jpg" },
                new Category() { Id = 9, Name = "TRX", Img_src = "trx.jpg" },
                new Category() { Id = 10, Name = "Úszás", Img_src = "swimming.jpg" },
                new Category() { Id = 11, Name = "Lovaglás", Img_src = "riding.jpg" },
                new Category() { Id = 12, Name = "Jóga", Img_src = "yoga.jpg" }

                );
            
            modelBuilder.Entity<Tag>().HasData(
                new Tag() { Id = 1, Name = "Csoportos", Colour = "#6610f2" },
                new Tag() { Id = 2, Name = "Saját testsúlyos", Colour = "#05A8AA" },
                new Tag() { Id = 3, Name = "Edzőterem", Colour = "red" },
                new Tag() { Id = 4, Name = "Szabadtéri", Colour = "black" },
                new Tag() { Id = 5, Name = "Zsírégető", Colour = "#0dcaf0" },
                new Tag() { Id = 6, Name = "Személyi edzés", Colour = "green" },
                new Tag() { Id = 7, Name = "Erőnléti", Colour = "#D7263D" },
                new Tag() { Id = 8, Name = "Aerobic", Colour = "blue" },
                new Tag() { Id = 9, Name = "Rehabilitációs", Colour = "#373F51" },
                new Tag() { Id = 10, Name = "Köredzés", Colour = "#9984D4" },
                new Tag() { Id = 11, Name = "Bemelegítés", Colour = "#F17300" },
                new Tag() { Id = 12, Name = "Flexibilitás", Colour = "#3A405A" }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Email = "jozsiedzo@email.com", Full_name = "Edző József", Id = 1, Location_id = 58, Password = "jozsi", Phone_number = "+36701234567", Role = "Trainer" },
                new User() { Email = "belaedzo@email.com", Full_name = "Edző Béla", Id = 2, Location_id = 59, Password = "bela", Phone_number = "+36701234566", Role = "Trainer" },
                new User() { Email = "gezaedzo@email.com", Full_name = "Edző Géza", Id = 3, Location_id = 60, Password = "geza", Phone_number = "+36701234565", Role = "Trainer" },
                new User() { Email = "jani@email.com", Full_name = "User János", Id = 4, Location_id = 61, Password = "jani", Phone_number = "+36701234564", Role = "User" },
                new User() { Email = "dani@email.com", Full_name = "User Dániel", Id = 5, Location_id = 62, Password = "dani", Phone_number = "+36701234563", Role = "User" },
                new User() { Email = "mari@email.com", Full_name = "User Mária", Id = 6, Location_id = 63, Password = "mari", Phone_number = "+36701234562", Role = "User" },
                new User() { Email = "evi@email.com", Full_name = "User Éva", Id = 7, Location_id = 64, Password = "evi", Phone_number = "+36701234561", Role = "User" },
                new User() { Email = "beni@email.com", Full_name = "User Benedek", Id = 8, Location_id = 65, Password = "beni", Phone_number = "+36701234560", Role = "User" },
                new User() { Email = "gabi@email.com", Full_name = "User Gabriella", Id = 9, Location_id = 66, Password = "gabi", Phone_number = "+36701234568", Role = "User" },
                new User() { Email = "admin@email.com", Full_name = "Admin", Id = 10, Location_id = 67, Password = "admin", Phone_number = "+36701234569", Role = "Admin" }
                );
            modelBuilder.Entity<Training>().HasData(
                new Training() { Id = 1, Category_id = 1, Contact_phone = "+36701234567", Description = "Rövid leírás az edzésről", Name = "Edzés 1", Trainer_id = 1 },
                new Training() { Id = 2, Category_id = 2, Contact_phone = "+36701234567", Description = "Rövid leírás az edzésről", Name = "Edzés 2", Trainer_id = 1 },
                new Training() { Id = 3, Category_id = 3, Contact_phone = "+36701234566", Description = "Rövid leírás az edzésről", Name = "Edzés 3", Trainer_id = 2 },
                new Training() { Id = 4, Category_id = 4, Contact_phone = "+36701234566", Description = "Rövid leírás az edzésről", Name = "Edzés 4", Trainer_id = 2 },
                new Training() { Id = 5, Category_id = 5, Contact_phone = "+36701234565", Description = "Rövid leírás az edzésről", Name = "Edzés 5", Trainer_id = 3 },
                new Training() { Id = 6, Category_id = 5, Contact_phone = "+36701234565", Description = "Rövid leírás az edzésről", Name = "Edzés 6", Trainer_id = 3 },
                new Training() { Id = 7, Category_id = 2, Contact_phone = "+36701234565", Description = "Rövid leírás az edzésről", Name = "Edzés 7", Trainer_id = 3 }
                );
            modelBuilder.Entity<TrainingSession>().HasData(
                new TrainingSession() { Address_name = "Virág utca 8.", Date = DateTime.Now, Id = 1, Location_id = 58, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 5, Place_name = "Sportközpont", Price = 1500, Training_id = 1 },
                new TrainingSession() { Address_name = "Virág utca 9.", Date = DateTime.Now, Id = 2, Location_id = 58, Max_member = 10, Min_member = 2, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 1 },
                new TrainingSession() { Address_name = "Virág utca 5.", Date = DateTime.Now, Id = 3, Location_id = 59, Max_member = 10, Min_member = 2, Minutes = 45, Number_of_applicants = 2, Place_name = "Sportközpont", Price = 1500, Training_id = 1 },
                new TrainingSession() { Address_name = "Virág utca 6.", Date = DateTime.Now, Id = 4, Location_id = 59, Max_member = 10, Min_member = 4, Minutes = 45, Number_of_applicants = 2, Place_name = "Sportközpont", Price = 1500, Training_id = 2 },
                new TrainingSession() { Address_name = "Virág utca 7.", Date = DateTime.Now, Id = 5, Location_id = 60, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 2 },
                new TrainingSession() { Address_name = "Virág utca 8.", Date = DateTime.Now, Id = 6, Location_id = 59, Max_member = 4, Min_member = 1, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 3 },
                new TrainingSession() { Address_name = "Virág utca 10.", Date = DateTime.Now, Id = 7, Location_id = 58, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 2, Place_name = "Sportközpont", Price = 1500, Training_id = 4 },
                new TrainingSession() { Address_name = "Virág utca 1.", Date = DateTime.Now, Id = 8, Location_id = 60, Max_member = 12, Min_member = 3, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 5 },
                new TrainingSession() { Address_name = "Virág utca 2.", Date = DateTime.Now, Id = 9, Location_id = 60, Max_member = 10, Min_member = 6, Minutes = 45, Number_of_applicants = 0, Place_name = "Sportközpont", Price = 1500, Training_id = 6 },
                new TrainingSession() { Address_name = "Virág utca 3.", Date = DateTime.Now, Id = 10, Location_id = 60, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 7 },
                new TrainingSession() { Address_name = "Virág utca 8.", Date = DateTime.Now, Id = 11, Location_id = 61, Max_member = 10, Min_member = 5, Minutes = 45, Number_of_applicants = 1, Place_name = "Sportközpont", Price = 1500, Training_id = 7 }
                );
            modelBuilder.Entity<Applicant>().HasData(
                new Applicant() { Id = 1, Training_session_id = 1, User_id = 4 },
                new Applicant() { Id = 2, Training_session_id = 2, User_id = 4 },
                new Applicant() { Id = 3, Training_session_id = 1, User_id = 5 },
                new Applicant() { Id = 4, Training_session_id = 3, User_id = 5 },
                new Applicant() { Id = 5, Training_session_id = 4, User_id = 6 },
                new Applicant() { Id = 6, Training_session_id = 5, User_id = 6 },
                new Applicant() { Id = 7, Training_session_id = 1, User_id = 7 },
                new Applicant() { Id = 8, Training_session_id = 7, User_id = 7 },
                new Applicant() { Id = 9, Training_session_id = 8, User_id = 7 },
                new Applicant() { Id = 10, Training_session_id = 1, User_id = 8 },
                new Applicant() { Id = 11, Training_session_id = 3, User_id = 9 },
                new Applicant() { Id = 12, Training_session_id = 1, User_id = 9 },
                new Applicant() { Id = 13, Training_session_id = 4, User_id = 9 },
                new Applicant() { Id = 14, Training_session_id = 10, User_id = 9 },
                new Applicant() { Id = 15, Training_session_id = 6, User_id = 9 },
                new Applicant() { Id = 16, Training_session_id = 7, User_id = 9 },
                new Applicant() { Id = 17, Training_session_id = 11, User_id = 9 }
                );
            modelBuilder.Entity<TagTraining>().HasData(
                new TagTraining() { Id = 1, Tag_id = 1, Training_id = 1 },
                new TagTraining() { Id = 2, Tag_id = 2, Training_id = 1 },
                new TagTraining() { Id = 3, Tag_id = 1, Training_id = 2 },
                new TagTraining() { Id = 4, Tag_id = 4, Training_id = 2 },
                new TagTraining() { Id = 5, Tag_id = 1, Training_id = 3 },
                new TagTraining() { Id = 6, Tag_id = 5, Training_id = 3 },
                new TagTraining() { Id = 7, Tag_id = 1, Training_id = 4 },
                new TagTraining() { Id = 8, Tag_id = 4, Training_id = 4 },
                new TagTraining() { Id = 9, Tag_id = 1, Training_id = 5 },
                new TagTraining() { Id = 10, Tag_id = 7, Training_id = 5 },
                new TagTraining() { Id = 11, Tag_id = 8, Training_id = 1 },
                new TagTraining() { Id = 12, Tag_id = 6, Training_id = 2 },
                new TagTraining() { Id = 13, Tag_id = 10, Training_id = 1 },
                new TagTraining() { Id = 14, Tag_id = 9, Training_id = 3 }
                );

            //string[] lines = File.ReadAllText("cities.csv").Trim().Split('\n');
            //int i = 1;
            //foreach (var line in lines)
            //{
            //    modelBuilder.Entity<Location>().HasData(
            //        new Location() { City_name = line.Split(';')[0].Trim(), County_name = line.Split(';')[1].Trim(), Id = i++ }
            //        );
            //}


        }

    }
}
