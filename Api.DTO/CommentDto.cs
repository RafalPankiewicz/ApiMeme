using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserCommID { get; set; }
        public int MemeID { get; set; }
        public string Contetnt { get; set; }
        public DateTime CreationDate { get; set; }
        public UserDto UserComm { get; set; }
    }
}
