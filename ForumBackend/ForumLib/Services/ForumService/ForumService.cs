using Dapper;
using ForumDAL.Models;
using ForumDAL.Repositories;
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
        private readonly IConfiguration Configuration;
        private readonly IStoreProcedure StoreProcedure;

        public ForumService(
            IConfiguration configuration,
            IStoreProcedure storeProcedure
            )
        {
            Configuration = configuration;
            StoreProcedure = storeProcedure;
        }
        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            return await StoreProcedure.GetPostsAsync();
        }

        public async Task<IEnumerable<PostDto>> GetPostsByFilterAsync(string key)
        {
            return await StoreProcedure.GetPostsAsync(key);
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int id)
        {
            return await StoreProcedure.GetRepliesByPostIdAsync(id);
        }
    }
}
