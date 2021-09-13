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

        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            return await PostStoreProcedure.GetPostsAsync();
        }

        public async Task<IEnumerable<PostDto>> GetPostsByFilterAsync(string key)
        {
            return await PostStoreProcedure.GetPostsAsync(key);
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id)
        {
            return await PostStoreProcedure.GetRepliesByPostIdAsync(id);
        }
    }
}
