using ForumLib.Dtos;
using ForumLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public interface IForumService
    {
        public Task<Result> AddPostAsync(int userId, string topic, string content);

        public Task<Result<IEnumerable<PostDto>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null);

        public Task<Result<IEnumerable<PostDto>>> GetPostsByFilterAsync(string key, int? pageIndex = null, int? pageSize = null);

        public Task<Result<ReplysDto>> GetRepliesByPostIdAsync(int id, int? pageIndex = null, int? pageSize = null);

        public Task<Result<ReplyDto>> AddReplyAsync(int postId, int userId, string content);

    }
}
