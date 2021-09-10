using ForumDAL.Models;
using ForumLib.Enums;
using ForumLib.Extensions;
using ForumLib.Models;
using ForumLib.Services.TokenService;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
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
                context.Result = new CustomActionResult(new Result((int)StatusCodeEnum.TokenNotExist));
                return;
            }

            var jti = context.HttpContext.User.GetJti();
            var level = context.HttpContext.User.GetPermission();

            var isValid = await TokenService.CheckJwtIsValidAsync(jti);

            if (!isValid || level < 1)
            {
                context.Result = new CustomActionResult(new Result((int)StatusCodeEnum.TokenExpired));
            }
        }
    }
}
