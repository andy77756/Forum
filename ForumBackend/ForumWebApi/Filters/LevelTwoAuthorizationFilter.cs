using ForumLib.Extensions;
using ForumLib.Services.TokenService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumWebApi.Filters
{
    public class LevelTwoAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenService TokenService;

        public LevelTwoAuthorizationFilter(ITokenService tokenService)
        {
            TokenService = tokenService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.Name is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var jti = context.HttpContext.User.GetJti();
            var level = context.HttpContext.User.GetPermission();

            var isValid = await TokenService.CheckJwtIsValidAsync(jti);

            if (!isValid || level < 2)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
