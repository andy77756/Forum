using Dapper;
using ForumDAL.Models;
using ForumDAL.Repositories;
using ForumLib.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public class ForumService : IForumService
    {
        private readonly IAuthorizeStoreProcedure AuthStoreProcedure;
        private readonly IPostStoreProcedure PostStoreProcedure;

        public ForumService(
            IAuthorizeStoreProcedure authStoreProcedure,
            IPostStoreProcedure postStoreProcedure
            )
        {
            AuthStoreProcedure = authStoreProcedure;
            PostStoreProcedure = postStoreProcedure;
        }

        public async Task<Result<PostDto>> AddPostAsync(int userId, string topic, string content)
        {
            var result = await PostStoreProcedure.AddPostAsync(userId, topic, content);
            return new Result<PostDto>(result.StatusCode);
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            return await AuthStoreProcedure.GetPostsAsync();
        }

        public async Task<IEnumerable<PostDto>> GetPostsByFilterAsync(string key)
        {
            return await AuthStoreProcedure.GetPostsAsync(key);
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id)
        {
            return await AuthStoreProcedure.GetRepliesByPostIdAsync(id);
        }
    }
}
