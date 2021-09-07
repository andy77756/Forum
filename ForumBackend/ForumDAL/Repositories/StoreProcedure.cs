using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class StoreProcedure : IStoreProcedure
    {
        private readonly IConfiguration Configuration;

        public StoreProcedure(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task AddLoginRecord(int userId)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                _ = await cn.QueryAsync("spTest", new { userId = userId }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                return await cn.QueryAsync<PostDto>("spGetPosts", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync(string key)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                return await cn.QueryAsync<PostDto>("spGetPosts", new { key = key}, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int postId)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                return await cn.QueryAsync<ReplyDto>("spGetReplies", new { postId = postId},commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
