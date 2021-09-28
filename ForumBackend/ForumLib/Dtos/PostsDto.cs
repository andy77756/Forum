using System.Collections.Generic;

namespace ForumLib.Dtos
{
    public class PostsDto
    {
        public IEnumerable<PostDto> Posts { get; set; }
        public MetaDataDto MetaData { get; set; }
    }
}
