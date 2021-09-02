using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Models
{
    public class User
    {
        public int f_id { get; set; }

        public string f_email { get; set; }

        public string f_userName { get; set; }

        public string f_password { get; set; }

        public DateTime f_createAt { get; set; }
    }
}
