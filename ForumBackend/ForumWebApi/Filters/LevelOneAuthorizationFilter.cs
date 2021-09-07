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
    public sealed class LevelOneAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenService TokenService;

        public LevelOneAuthorizationFilter(ITokenService tokenService)
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

            if (!isValid || level < 1)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
