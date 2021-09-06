using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Helpers;
using ForumLib.Services.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService TokenService;
        private readonly IUserRepository UserRepository;
        private readonly EncryptHelper EncryptHelper;

        /// <summary>
        /// constructor
        /// </summary>
        public LoginService(
            ITokenService tokenService, 
            IUserRepository userRepository,
            EncryptHelper encryptHelper)
        {
            TokenService = tokenService;
            UserRepository = userRepository;
            EncryptHelper = encryptHelper;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="pwd">password</param>
        /// <returns></returns>
        public async Task<UserInfoDto> LoginAsync(string userName, string pwd)
        {
            var user = await UserRepository.GetByUserNameAsync(userName);

            var encrytPwd = EncryptHelper.Encrypt(pwd);

            if (user.f_pwd != encrytPwd)
            {
                throw new Exception("wrong password");
            }

            var jwt = await TokenService.GenerateJwtAsync(user.f_id, user.f_level);

            return new UserInfoDto
            {
                UserId = user.f_id,
                UserName = user.f_userName,
                Nickname = user.f_nickname,
                Token = jwt
            };
        }

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
