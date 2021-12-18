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
