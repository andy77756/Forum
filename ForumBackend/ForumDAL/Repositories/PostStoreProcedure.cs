using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class PostStoreProcedure : IPostStoreProcedure
    {
        private readonly IConfiguration Configuration;

        public PostStoreProcedure(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<QueryResult<PostDto>> AddPostAsync(int userId, string topic, string content)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userId", userId);
                param.Add("@topic", topic);
                param.Add("@content", content);
                param.Add("@returnValue", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                var result = await cn.QueryAsync<PostDto>("spAddPost", param,commandType: System.Data.CommandType.StoredProcedure);
                return new QueryResult<PostDto>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = null
                };
            }
        }
    }
}
