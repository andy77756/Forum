using System.Collections.Generic;

namespace ForumLib.Dtos
{
    public class ReplysDto
    {
        public PostDto Post { get; set; }
        public IEnumerable<ReplyDto> Replies { get; set; }
    }
}
