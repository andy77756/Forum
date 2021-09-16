using ForumDAL.Models;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IAuthorizeStoreProcedure
    {
        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="userName">帳號</param>
        /// <param name="nickname">暱稱</param>
        /// <param name="pwd">密碼</param>
        /// <returns></returns>
        public Task<QueryResult<User>> RegisterAsync(string userName, string nickname, string pwd);

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userName">帳號</param>
        /// <param name="pwd">密碼</param>
        /// <returns></returns>
        public Task<QueryResult<User>> LoginAsync(string userName, string pwd);

        
    }
}
