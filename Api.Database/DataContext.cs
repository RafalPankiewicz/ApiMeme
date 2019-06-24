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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Meme>();
            modelBuilder.Entity<Comment>();
            modelBuilder.Entity<Comment>().HasOne(t => t.UserComm).
                WithMany(a => a.Comments).OnDelete(DeleteBehavior.ClientSetNull); 

            modelBuilder.Entity<Comment>().HasOne(t => t.Meme).
                WithMany(a => a.Comments);

            modelBuilder.Entity<Meme>().HasOne(t => t.User).
               WithMany(a => a.Memes);
        }
    }
}
