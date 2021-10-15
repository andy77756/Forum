using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum_v2.ajax
{
    public class TokenValidator
    {
        /// <summary>
        /// 驗證token是否過期
        /// </summary>
        /// <param name="expireDt"></param>
        /// <returns></returns>
        public bool IsExpired(string expireDt)
        {
             if (DateTime.Now > ToDatetime(expireDt))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 驗證使否為Level1
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsLevelOne(string level)
        {
            if (Convert.ToInt32(level) >= 1 )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 驗證使否為Level2
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsLevelTwo(string level)
        {
            if (Convert.ToInt32(level) >= 2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 驗證並取得JWT Claim
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetClaimPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validateParam = new TokenValidationParameters
            {
                RequireExpirationTime = false,
                ValidateAudience = false,
                ValidIssuer = "Forum",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789109148387491")),
                ValidateLifetime = false
            };
            
            var validResult = tokenHandler.ValidateToken(token, validateParam, out var securityToken);

            return validResult;
        }

        /// <summary>
        /// Unix時間轉為Datetime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime ToDatetime(string str)
        {
            var unixTimeStamp = Convert.ToDouble(str);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}