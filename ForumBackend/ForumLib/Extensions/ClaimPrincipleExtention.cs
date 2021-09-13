using System;
using System.Linq;
using System.Security.Claims;

namespace ForumLib.Extensions
{
    /// <summary>
    /// 針對 ClaimsPrincipal 物件擴充類別
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// 取得 UserID
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        public static int GetID(this ClaimsPrincipal user) => user.Identity.Name.ToInt() ?? 0;

        /// <summary>
        /// 取得 Jti
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        public static string GetJti(this ClaimsPrincipal user) => user.Claims.FirstOrDefault(x => x.Type == "jti")?.Value;

        /// <summary>
        /// 取得 permission
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        public static int GetPermission(this ClaimsPrincipal user) => user.Claims.FirstOrDefault(x => x.Type == "level")?.Value.ToInt() ?? 0;

        /// <summary>
        /// 取得ExpireTime
        /// </summary>
        /// <param name="user">user</param>
        /// <returns></returns>
        public static DateTime GetExpireTime(this ClaimsPrincipal user) => user.Claims.FirstOrDefault(x => x.Type == "exp").Value.ToDatetime();
    }
}
