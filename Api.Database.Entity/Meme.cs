using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.Database.Entity
{
    public class Meme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }

        public string PhotoName { get; set; }

        public int Rate { get; set; }

        public DateTime CerationDate { get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<Comment> comments { get; set; }
    }
}
