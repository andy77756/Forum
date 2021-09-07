using ForumDAL.Repositories;
using ForumLib.Helpers;
using ForumLib.Models;
using System;
using System.Collections.Concurrent;
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
        /// User Repository
        /// </summary>
        private readonly IUserRepository UserRepository;

        /// <summary>
        /// Token map to expiredDatetime and permission
        /// </summary>
        private ConcurrentDictionary<string, TokenMapInfo> tokenMaps = new ConcurrentDictionary<string, TokenMapInfo>();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="jwtHelper">jwthelper</param>
        public TokenService(JwtHelper jwtHelper, IUserRepository userRepository)
        {
            JwtHelper = jwtHelper;
            UserRepository = userRepository;
        }

        public async Task<bool> CheckJwtIsValidAsync(string jti)
        {
            var isTrue = tokenMaps.TryGetValue(jti, out TokenMapInfo info);

            if (!isTrue)
            {
                return false;
            }

            if (info.ExpiredDateTime < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteJtiAsync(string jti)
        {
            _ = tokenMaps.TryRemove(jti, out var info);

            return;
        }

        public async Task<string> GenerateJwtAsync(int userId, int level)
        {
            var jti = Guid.NewGuid().ToString();

            var expiredDt = DateTime.Now.AddHours(1);

            var jwt = JwtHelper.GenerateJwt(jti, userId, expiredDt, level);

            tokenMaps.TryAdd(jti, new TokenMapInfo
            {
                ExpiredDateTime = expiredDt,
                Permission = level
            });

            return jwt;
        }

    }
}
