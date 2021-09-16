using System;

namespace ForumDAL.Models
{
    public class Reply
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string UserName { get; set; }

        public string Nickname { get; set; }
    }
}
