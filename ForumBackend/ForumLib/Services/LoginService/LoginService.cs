using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Enums;
using ForumLib.Helpers;
using ForumLib.Models;
using ForumLib.Services.TokenService;
using System;
using System.Threading.Tasks;

namespace ForumLib.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService TokenService;
        private readonly EncryptHelper EncryptHelper;
        private readonly IAuthorizeStoreProcedure StoreProcedure;

        /// <summary>
        /// constructor
        /// </summary>
        public LoginService(
            ITokenService tokenService, 
            EncryptHelper encryptHelper,
            IAuthorizeStoreProcedure storeProcedure)
        {
            TokenService = tokenService;
            EncryptHelper = encryptHelper;
            StoreProcedure = storeProcedure;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="pwd">password</param>
        /// <returns></returns>
        public async Task<Result<UserInfoDto>> LoginAsync(string userName, string pwd)
        {
            var loginResult = await StoreProcedure.LoginAsync(userName, EncryptHelper.Encrypt(pwd));

            if (loginResult.StatusCode != (int)StatusCodeEnum.Success)
            {
                return new Result<UserInfoDto>(loginResult.StatusCode);
            }

            var jwt = await TokenService.GenerateJwtAsync(loginResult.Result.Id, loginResult.Result.Level);

            var returnData = new Result<UserInfoDto>(loginResult.StatusCode, new UserInfoDto
            {
                UserId = loginResult.Result.Id,
                UserName = loginResult.Result.UserName,
                Nickname = loginResult.Result.Nickname,
                Level = loginResult.Result.Level,
                Token = jwt
            });

            return returnData;
        }

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public Task<Result> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
