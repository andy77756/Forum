using System;

namespace ForumDAL.Models
{
    public class PostDto
    {
        public int? f_postid { get; set; }

        public string f_topic { get; set; }

        public string f_postUserName { get; set; }

        public DateTime? f_postDt { get; set; }

        public string f_replyUserName { get; set; }

        public DateTime? f_replyDt { get; set; }

    }
}
