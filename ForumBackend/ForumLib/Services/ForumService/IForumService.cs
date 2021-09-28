using ForumLib.Dtos;
using ForumLib.Models;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public interface IForumService
    {
        public Task<Result> AddPostAsync(int userId, string topic, string content);

        public Task<Result<PostsDto>> GetPostsAsync(string keyTopic, string keyNickname,int? pageIndex = null, int? pageSize = null);

        public Task<Result<ReplysDto>> GetRepliesByPostIdAsync(int id, int? pageIndex = null, int? pageSize = null);

        public Task<Result<ReplyDto>> AddReplyAsync(int postId, int userId, string content);

    }
}
