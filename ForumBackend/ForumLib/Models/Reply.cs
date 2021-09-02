using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Models
{
    public class Reply
    {
        public int f_id { get; set; }

        public int f_userId { get; set; }

        public int f_postId { get; set; }

        public string f_content { get; set; }

        public DateTime f_createAt { get; set; }
    }
}
