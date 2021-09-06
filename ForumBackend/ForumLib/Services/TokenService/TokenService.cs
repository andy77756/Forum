using ForumLib.Helpers;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ForumLib.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly JwtHelper JwtHelper;
        private ConcurrentDictionary<string, DateTime> tokenMaps = new ConcurrentDictionary<string, DateTime>();

        public TokenService(JwtHelper jwtHelper)
        {
            JwtHelper = jwtHelper;
        }

        public async Task<bool> CheckJwtIsValidAsync(string jti)
        {
            var isTrue = tokenMaps.TryGetValue(jti, out DateTime expiredDt);

            if (!isTrue)
            {
                return false;
            }

            if (expiredDt < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteJtiAsync(string jti)
        {
            _ = tokenMaps.TryRemove(jti, out var dt);

            return;
        }

        public async Task<string> GenerateJwtAsync(int userId)
        {
            var jti = Guid.NewGuid().ToString();

            var expiredDt = DateTime.Now.AddHours(1);

            var jwt = JwtHelper.GenerateJwt(jti, userId, expiredDt, null);

            tokenMaps.TryAdd(jti, expiredDt);

            return jwt;
        }
    }
}
