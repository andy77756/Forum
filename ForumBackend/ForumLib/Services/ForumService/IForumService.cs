using ForumDAL.Models;
using ForumLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public interface IForumService
    {
        public Task<Result<PostDto>> AddPostAsync(int userId, string topic, string content);
        public Task<Result<IEnumerable<PostDto>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null);

        public Task<Result<IEnumerable<PostDto>>> GetPostsByFilterAsync(string key, int? pageIndex = null, int? pageSize = null);

        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id);

    }
}
