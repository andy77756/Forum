using ForumDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public interface IForumService
    {
        public Task<IEnumerable<PostDto>> GetPostsAsync();

        public Task<IEnumerable<PostDto>> GetPostsByFilterAsync(string key);

        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id);

    }
}
