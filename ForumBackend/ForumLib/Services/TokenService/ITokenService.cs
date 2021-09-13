using System;
using System.Threading.Tasks;

namespace ForumLib.Services.TokenService
{
    public interface ITokenService
    {
        /// <summary>
        /// 產生JWT token
        /// </summary>
        /// <param name="userId">使用者ID</param>
        /// <param name="level">權限等級</param>
        /// <returns></returns>
        public Task<string> GenerateJwtAsync(int userId, int level);

        /// <summary>
        /// 確認token是否過期
        /// </summary>
        /// <param name="exp">過期時間</param>
        /// <returns></returns>
        public Task<bool> CheckJwtIsValidAsync(DateTime exp);       
    }
}
