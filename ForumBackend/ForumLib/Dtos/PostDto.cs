using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Dtos
{
    public class PostDto
    {
        public int? Postid { get; set; }

        public string Topic { get; set; }

        public string PostUserName { get; set; }

        public string PostAt { get; set; }

        public string ReplyUserName { get; set; }

        public string ReplyDt { get; set; }

        public int ReplyCount { get; set; }
    }
}
