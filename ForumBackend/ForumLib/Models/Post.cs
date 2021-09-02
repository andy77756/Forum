using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Models
{
    public class Post
    {
        public int f_id { get; set; }

        public int f_userId { get; set; }

        public string f_topic { get; set; }

        public string f_content { get; set; }

        public DateTime f_CreateAt { get; set; }
    }
}
