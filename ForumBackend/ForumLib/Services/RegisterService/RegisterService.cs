using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Enums;
using ForumLib.Helpers;
using ForumLib.Models;
using ForumLib.Services.TokenService;
using System.Threading.Tasks;

namespace ForumLib.Services.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly ITokenService TokenService;
        private readonly EncryptHelper EncryptHelper;
        private readonly IAuthorizeStoreProcedure StoreProcedure;

        public RegisterService(
            ITokenService tokenService,
            EncryptHelper encryptHelper,
            IAuthorizeStoreProcedure storeProcedure)
        {
            TokenService = tokenService;
            EncryptHelper = encryptHelper;
            StoreProcedure = storeProcedure;
        }
        public async Task<Result<UserInfoDto>> RegisterAsync(string userName, string nickname, string pwd)
        {
            var registerResult = await StoreProcedure.RegisterAsync(
                                                userName,
                                                nickname,
                                                EncryptHelper.Encrypt(pwd));
            if (registerResult.StatusCode != (int)StatusCodeEnum.Success)
            {
                return new Result<UserInfoDto>(registerResult.StatusCode);
            }

            var jwt = await TokenService.GenerateJwtAsync(registerResult.Result.Id, registerResult.Result.Level);

            var result = new Result<UserInfoDto>(registerResult.StatusCode, new UserInfoDto
            {
                UserId = registerResult.Result.Id,
                UserName = userName,
                Nickname = nickname,
                Level = registerResult.Result.Level,
                Token = jwt
            });

            return result;

        }
    }
}
