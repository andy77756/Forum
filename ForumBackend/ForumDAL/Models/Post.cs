using System;

namespace ForumDAL.Models
{
    public class Post
    {
        public int f_id { get; set; }

        public int f_userId { get; set; }

        public string f_topic { get; set; }

        public string f_content { get; set; }

        public DateTime f_createAt { get; set; }

        public DateTime? f_updateAt { get; set; }
    }
}
