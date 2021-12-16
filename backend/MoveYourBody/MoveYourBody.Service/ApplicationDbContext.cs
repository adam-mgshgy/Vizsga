using Microsoft.EntityFrameworkCore;
using System;

namespace MoveYourBody.Service
{
    public class ApplicationDbContext: DbContext
    {
        private readonly string connectionString;

        public DbSet<User> Users { get; set; }

        public ApplicationDbContext()
        {
#if DEBUG        
            connectionString = "Server=localhost;Database=NyitottKapukRegisztracio;Uid=root;Pwd=;";
#endif
        }

    }
}
