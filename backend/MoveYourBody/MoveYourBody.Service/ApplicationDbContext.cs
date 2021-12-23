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
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<TagTraining> TagTraining { get; set; }
        public DbSet<TrainingSession> TrainingSession { get; set; }

        public ApplicationDbContext()
        {
#if DEBUG        
            connectionString = "Server=localhost;Database=moveyourbody;Uid=root;Pwd=;";
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
            modelBuilder.Entity<Applicant>().HasNoKey();
            modelBuilder.Entity<Category>().HasData(
                new Category() { Name = "Box", Img_src = "box.jpg" },
                new Category() { Name = "Crossfit", Img_src = "crossFitt.jpg" },
                new Category() { Name = "Labdarúgás", Img_src = "football.jpg" },
                new Category() { Name = "Kosárlabda", Img_src = "basketball.jpg" },
                new Category() { Name = "Kézilabda", Img_src = "handball.jpg" },
                new Category() { Name = "Röplabda", Img_src = "volleyball.jpg" },
                new Category() { Name = "Spartan", Img_src = "spartan.jpg" },
                new Category() { Name = "Tenisz", Img_src = "tennis.jpg" },
                new Category() { Name = "TRX", Img_src = "trx.jpg" },
                new Category() { Name = "Úszás", Img_src = "swimming.jpg" },
                new Category() { Name = "Lovaglás", Img_src = "riding.jpg" },
                new Category() { Name = "Jóga", Img_src = "yoga.jpg" }

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
