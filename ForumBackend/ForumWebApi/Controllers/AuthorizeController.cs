using ForumLib.Services.LoginService;
using ForumLib.Services.RegisterService;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
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
            var result = await RegisterService.RegisterAsync(registerInfo.UserName, registerInfo.Nickname, registerInfo.Pwd);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginInfo loginInfo)
        {
            var result = await LoginService.LoginAsync(loginInfo.UserName, loginInfo.Pwd);
            return Ok(result);
        }
    }
}
