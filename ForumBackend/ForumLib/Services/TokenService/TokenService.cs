using ForumLib.Helpers;
using System;
using System.Threading.Tasks;

namespace ForumLib.Services.TokenService
{
    public class TokenService : ITokenService
    {
        /// <summary>
        /// JwtHelper
        /// </summary>
        private readonly JwtHelper JwtHelper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="jwtHelper">jwthelper</param>
        public TokenService(JwtHelper jwtHelper)
        {
            JwtHelper = jwtHelper;
        }

        /// <summary>
        /// 確認token是否過期
        /// </summary>
        /// <param name="exp">過期時間</param>
        /// <returns></returns>
        public async Task<bool> CheckJwtIsValidAsync(DateTime exp)
        {           
            if (exp < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 產生JWT token
        /// </summary>
        /// <param name="userId">使用者ID</param>
        /// <param name="level">權限等級</param>
        /// <returns></returns>
        public async Task<string> GenerateJwtAsync(int userId, int level)
        {
            var jti = Guid.NewGuid().ToString();

            var expiredDt = DateTime.Now.AddHours(1);

            var jwt = JwtHelper.GenerateJwt(jti, userId, expiredDt, level);

            return jwt;
        }

    }
}
