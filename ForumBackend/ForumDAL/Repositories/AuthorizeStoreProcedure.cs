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
    public class AuthorizeStoreProcedure : IAuthorizeStoreProcedure
    {
        private readonly IConfiguration Configuration;

        public AuthorizeStoreProcedure(IConfiguration configuration)
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

        public async Task<QueryResult<User>> LoginAsync(string userName, string pwd)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userName", userName);
                param.Add("@pwd", pwd);
                param.Add("@returnValue", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                var result = await cn.QuerySingleOrDefaultAsync<User>("spLogin", param, commandType: System.Data.CommandType.StoredProcedure);
                return new QueryResult<User>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = result
                };
            }
        }

        public async Task<QueryResult<User>> RegisterAsync(string userName, string nickname, string pwd)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userName", userName);
                param.Add("@nickname", nickname);
                param.Add("@pwd", pwd);
                param.Add("@returnValue", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                var result = await cn.QuerySingleOrDefaultAsync<User>("spRegister", param, commandType: System.Data.CommandType.StoredProcedure);
                return new QueryResult<User>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = result
                };
            }
        }
    }
}
