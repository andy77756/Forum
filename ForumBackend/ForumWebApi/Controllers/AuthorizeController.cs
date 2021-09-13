using ForumLib.Enums;
using ForumLib.Models;
using ForumLib.Services.LoginService;
using ForumLib.Services.RegisterService;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        public async Task<IActionResult> RegisterAsync(RegisterInfo registerInfo)
        {
            var nameRex = new Regex("^.[A-Za-z0-9]+$");
            if (
                string.IsNullOrEmpty(registerInfo.UserName) || 
                registerInfo.UserName.Length > 30 || 
                registerInfo.UserName.Length < 5 || 
                !nameRex.Match(registerInfo.UserName).Success)
            {
                return Ok(new Result((int)StatusCodeEnum.UserNameInValid));
            }

            if (
                string.IsNullOrEmpty(registerInfo.Nickname) ||
                registerInfo.Nickname.Length > 10 ||
                registerInfo.Nickname.Length < 1 ||
                !nameRex.Match(registerInfo.Nickname).Success)
            {
                return Ok(new Result((int)StatusCodeEnum.NicknameInvalid));
            }

            if (
                string.IsNullOrEmpty(registerInfo.Pwd) || 
                registerInfo.Pwd.Length > 20 || 
                registerInfo.Pwd.Length < 6)
            {
                return Ok(new Result((int)StatusCodeEnum.PwdInvalid));
            }

            var result = await RegisterService.RegisterAsync(registerInfo.UserName, registerInfo.Nickname, registerInfo.Pwd);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginInfo loginInfo)
        {
            var userNameRex = new Regex("^.[A-Za-z0-9]+$");
            if (
                string.IsNullOrEmpty(loginInfo.UserName) || 
                loginInfo.UserName.Length > 30 || 
                loginInfo.UserName.Length < 5 || 
                !userNameRex.Match(loginInfo.UserName).Success)
            {
                return Ok(new Result((int)StatusCodeEnum.UserNameInValid));
            }

            if (
                string.IsNullOrEmpty(loginInfo.Pwd) || 
                loginInfo.Pwd.Length > 20 || 
                loginInfo.Pwd.Length < 6)
            {
                return Ok(new Result((int)StatusCodeEnum.PwdInvalid));
            }

            var result = await LoginService.LoginAsync(loginInfo.UserName, loginInfo.Pwd);
            return Ok(result);
        }
    }
}
