using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Models
{
    public class ReplyDto
    {
        public int f_id { get; set; }

        public string f_content { get; set; }

        public DateTime f_createAt { get; set; }

        public DateTime? f_updateAt { get; set; }

        public string f_userName { get; set; }

        public string f_nickname { get; set; }
    }
}
