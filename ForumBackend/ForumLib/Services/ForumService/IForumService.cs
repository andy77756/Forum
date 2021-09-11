using ForumDAL.Models;
using ForumLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public interface IForumService
    {
        public Task<Result<PostDto>> AddPostAsync(int userId, string topic, string content);
        public Task<IEnumerable<PostDto>> GetPostsAsync();

        public Task<IEnumerable<PostDto>> GetPostsByFilterAsync(string key);

        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id);

    }
}
