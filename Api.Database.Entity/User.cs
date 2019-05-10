using System;
using System.Collections.Generic;

namespace Api.Database.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }

        public virtual IEnumerable<Meme> Memes { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
