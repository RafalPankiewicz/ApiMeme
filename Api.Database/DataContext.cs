using Api.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
       public DbSet<Meme> Memes { get; set; }
        public DbSet<Comment>   Comments { get; set; }
    }
}
