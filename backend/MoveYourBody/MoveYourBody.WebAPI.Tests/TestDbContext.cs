using Microsoft.EntityFrameworkCore;
using MoveYourBody.Service;
using System;

namespace MoveYourBody.WebAPI.Tests
{
    internal class TestDbContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase($"moveyourbody{Guid.NewGuid()}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public static TestDbContext GenerateTestDbContext()
        {
            var context = new TestDbContext();
            context.Database.EnsureCreated();
            return context;
        }
    }
}