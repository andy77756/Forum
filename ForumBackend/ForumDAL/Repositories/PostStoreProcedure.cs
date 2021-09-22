using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class PostStoreProcedure : IPostStoreProcedure
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 建構式注入IConfiguration
        /// </summary>
        /// <param name="configuration">configuration</param>
        public PostStoreProcedure(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="topic">標題</param>
        /// <param name="content">文章內容</param>
        /// <returns></returns>
        public async Task<QueryResult<Post>> AddPostAsync(int userId, string topic, string content)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userId", userId);
                param.Add("@topic", topic);
                param.Add("@content", content);
                param.Add("@returnValue", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);
                var result = await cn.QueryAsync<Post>("spAddPost", param, commandType: CommandType.StoredProcedure);
                return new QueryResult<Post>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = null
                };
            }
        }

        /// <summary>
        /// 新增回覆
        /// </summary>
        /// <param name="postId">文章id</param>
        /// <param name="userId">發文者userId</param>
        /// <param name="content">回覆內容</param>
        /// <returns></returns>
        public async Task<QueryResult<Reply>> AddReplyAsync(int postId, int userId, string content)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userId", userId);
                param.Add("@postId", postId);
                param.Add("@content", content);
                param.Add("@returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                var result = await cn.QuerySingleAsync<Reply>("spAddReply", param, commandType: CommandType.StoredProcedure);

                return new QueryResult<Reply>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = result
                };
            }
        }

        /// <summary>
        /// 取得文章列表
        /// </summary>
        /// <returns></returns>
        public async Task<QueryResult<IEnumerable<Post>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await cn.QueryAsync<Post>(
                    "spGetPosts",
                    new { pageIndex = pageIndex ?? 1, pageSize = pageSize ?? 10 },
                    commandType: CommandType.StoredProcedure);

                return new QueryResult<IEnumerable<Post>>
                {
                    StatusCode = 1,
                    Result = result
                };
            }
        }

        /// <summary>
        /// 取得篩選後文章列表
        /// </summary>
        /// <param name="key">關鍵字</param>
        /// <returns></returns>
        public async Task<QueryResult<IEnumerable<Post>>> GetPostsAsync(string key, int? pageIndex = null, int? pageSize = null)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await cn.QueryAsync<Post>(
                    "spGetPosts",
                    new { key = key, pageIndex = pageIndex ?? 1, pageSize = pageSize ?? 10 },
                    commandType: CommandType.StoredProcedure);

                return new QueryResult<IEnumerable<Post>>
                {
                    StatusCode = 1,
                    Result = result
                };
            }
        }

        /// <summary>
        /// 取得文章回覆內容
        /// </summary>
        /// <param name="postId">文章id</param>
        /// <returns></returns>
        public async Task<QueryResult<Replys>> GetRepliesByPostIdAsync(int postId, int? pageIndex = null, int? pageSize = null)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@postId", postId);
                param.Add("@pageIndex", pageIndex ?? 1);
                param.Add("@pageSize", pageSize ?? 10);
                param.Add("@returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                var reader = await cn.QueryMultipleAsync(
                                        "spGetReplies",
                                        param,
                                        commandType: CommandType.StoredProcedure);

                
                return new QueryResult<Replys>
                {
                    Result = new Replys
                    {
                        Post = await reader.ReadSingleOrDefaultAsync<Post>(),
                        Replies = await reader.ReadAsync<Reply>()
                    },
                    StatusCode = param.Get<int>("@returnValue")
                };              
            }
        }
    }
}
