using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    /// <summary>
    /// 驗證SP
    /// </summary>
    public class AuthorizeStoreProcedure : IAuthorizeStoreProcedure
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 建構式注入IConfiguration
        /// </summary>
        /// <param name="configuration">configuration</param>
        public AuthorizeStoreProcedure(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userName">帳號</param>
        /// <param name="pwd">密碼</param>
        /// <returns></returns>
        public async Task<QueryResult<User>> LoginAsync(string userName, string pwd)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userName", userName);
                param.Add("@pwd", pwd);
                param.Add("@returnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                var result = await cn.QuerySingleOrDefaultAsync<User>("spLogin", param, commandType: CommandType.StoredProcedure);
                return new QueryResult<User>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = result
                };
            }
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="userName">帳號</param>
        /// <param name="nickname">暱稱</param>
        /// <param name="pwd">密碼</param>
        /// <returns></returns>
        public async Task<QueryResult<User>> RegisterAsync(string userName, string nickname, string pwd)
        {
            using (var cn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var param = new DynamicParameters();
                param.Add("@userName", userName);
                param.Add("@nickname", nickname);
                param.Add("@pwd", pwd);
                param.Add("@returnValue", dbType: System.Data.DbType.Int32, direction: ParameterDirection.ReturnValue);
                var result = await cn.QuerySingleOrDefaultAsync<User>("spRegister", param, commandType: CommandType.StoredProcedure);
                return new QueryResult<User>
                {
                    StatusCode = param.Get<int>("@returnValue"),
                    Result = result
                };
            }
        }
    }
}
