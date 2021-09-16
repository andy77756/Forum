using System.Collections.Generic;

namespace ForumDAL.Models
{
    public class Replys
    {
        public Post Post { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}
