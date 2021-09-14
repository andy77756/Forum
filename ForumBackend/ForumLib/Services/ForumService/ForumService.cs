using ForumDAL.Models;
using ForumDAL.Repositories;
using ForumLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public class ForumService : IForumService
    {
        private readonly IPostStoreProcedure PostStoreProcedure;

        public ForumService(
            IPostStoreProcedure postStoreProcedure
            )
        {
            PostStoreProcedure = postStoreProcedure;
        }

        public async Task<Result<PostDto>> AddPostAsync(int userId, string topic, string content)
        {
            var result = await PostStoreProcedure.AddPostAsync(userId, topic, content);
            return new Result<PostDto>(result.StatusCode);
        }

        public async Task<Result<IEnumerable<PostDto>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetPostsAsync(pageIndex, pageSize);

            return new Result<IEnumerable<PostDto>>(result.StatusCode, result.Result);
        }

        public async Task<Result<IEnumerable<PostDto>>> GetPostsByFilterAsync(string key, int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetPostsAsync(key, pageIndex, pageSize);

            return new Result<IEnumerable<PostDto>>(result.StatusCode, result.Result);
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id)
        {
            return await PostStoreProcedure.GetRepliesByPostIdAsync(id);
        }
    }
}
