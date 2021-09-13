using ForumLib.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumLib.Helpers
{
    public class JwtHelper
    {
        private readonly JwtConfig _jwtConfig;

        public JwtHelper(IOptions<JwtConfig> jwtOptions)
        {
            _jwtConfig = jwtOptions.Value;
        }

        /// <summary>
        /// 產生 JWT
        /// </summary>
        /// <param name="jti">Jti</param>
        /// <param name="id">使用者ID</param>
        /// <param name="expiredDateTime">過期時間</param>
        /// <param name="level">permission</param>
        /// <returns></returns>
        public string GenerateJwt(string jti, int id, DateTime expiredDateTime, int level)
        {
            if (expiredDateTime < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(expiredDateTime), message: "過期時間需大於現在時間");
            }

            var userClaims = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim("level", level.ToString())
            });

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SignKey));

            // Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.Issuer,
                Subject = userClaims,
                Expires = expiredDateTime,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return serializeToken;
        }
    }
}
