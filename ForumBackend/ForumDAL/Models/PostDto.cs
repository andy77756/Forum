using System;

namespace ForumDAL.Models
{
    public class PostDto
    {
        public int? Postid { get; set; }

        public string Topic { get; set; }

        public string PostUserName { get; set; }

        public DateTime? PostAt { get; set; }

        public string ReplyUserName { get; set; }

        public DateTime? ReplyDt { get; set; }

    }
}
