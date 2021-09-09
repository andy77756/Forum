using ForumLib.Dtos;
using ForumLib.Services.LoginService;
using ForumLib.Services.RegisterService;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ForumWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IRegisterService RegisterService;

        private readonly ILoginService LoginService;

        public AuthorizeController(IRegisterService registerService, ILoginService loginService)
        {
            RegisterService = registerService;
            LoginService = loginService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserInfoDto>> RegisterAsync(RegisterInfo registerInfo)
        {
            try
            {
                var result = await RegisterService.RegisterAsync(registerInfo.UserName, registerInfo.Nickname, registerInfo.Pwd);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserInfoDto>> LoginAsync(LoginInfo loginInfo)
        {
            try
            {
                return await LoginService.LoginAsync(loginInfo.UserName, loginInfo.Pwd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
