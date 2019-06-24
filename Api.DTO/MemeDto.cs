using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class MemeDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string PhotoName { get; set; }
        public int Rate { get; set; }
        public DateTime CerationDate { get; set; }
        public virtual UserDto User { get; set; }
    }
}
