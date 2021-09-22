using System;

namespace ForumDAL.Models
{
    public class Post
    {
        public int? Postid { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public string PostUserName { get; set; }

        public DateTime? PostAt { get; set; }

        public string ReplyUserName { get; set; }

        public DateTime? ReplyAt { get; set; }

        public int ReplyCount { get; set; }

    }
}
