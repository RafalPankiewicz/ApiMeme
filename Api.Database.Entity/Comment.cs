using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.Database.Entity
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int UserCommID { get; set; }
        public int MemeID { get; set; }
        public string Contetnt { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("UserCommID")]
        public virtual User UserComm { get; set; }
        public virtual Meme Meme { get; set; }
    }
}
