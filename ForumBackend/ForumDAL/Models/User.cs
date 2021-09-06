using System;

namespace ForumDAL.Models
{
    public class User
    {
        public int f_id { get; set; }

        public string f_nickname { get; set; }

        public string f_userName { get; set; }

        public string f_pwd { get; set; }

        public DateTime f_createAt { get; set; }

        public int f_level { get; set; }
    }
}
