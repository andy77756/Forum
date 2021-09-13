using ForumLib.Enums;
using ForumLib.Extensions;
using ForumLib.Models;
using ForumLib.Services.TokenService;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ForumWebApi.Filters
{
    /// <summary>
    /// 等級2驗證filter
    /// </summary>
    public class LevelTwoAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// TokenService
        /// </summary>
        private readonly ITokenService TokenService;

        /// <summary>
        /// 建構式注入TokenService
        /// </summary>
        /// <param name="tokenService">TokenService</param>
        public LevelTwoAuthorizationFilter(ITokenService tokenService)
        {
            TokenService = tokenService;
        }

        /// <summary>
        /// 驗證方法
        /// </summary>
        /// <param name="context">contex</param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.Name is null)
            {
                context.Result = new CustomActionResult(new Result((int)StatusCodeEnum.TokenNotExist));
                return;
            }

            var level = context.HttpContext.User.GetPermission();
            var exp = context.HttpContext.User.GetExpireTime();

            var isValid = await TokenService.CheckJwtIsValidAsync(exp);

            if (!isValid)
            {
                context.Result = new CustomActionResult(new Result((int)StatusCodeEnum.TokenExpired));
            }

            if (level < 2)
            {
                context.Result = new CustomActionResult(new Result((int)StatusCodeEnum.PermissionDeny));
            }
        }
    }
}
