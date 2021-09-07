using ForumDAL.Models;
using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Helpers;
using ForumLib.Services.TokenService;
using System;
using System.Threading.Tasks;

namespace ForumLib.Services.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly ITokenService TokenService;
        private readonly IUserRepository UserRepository;
        private readonly EncryptHelper EncryptHelper;
        private readonly IStoreProcedure StoreProcedure;

        public RegisterService(
            ITokenService tokenService,
            IUserRepository userRepository,
            EncryptHelper encryptHelper,
            IStoreProcedure storeProcedure)
        {
            TokenService = tokenService;
            UserRepository = userRepository;
            EncryptHelper = encryptHelper;
            StoreProcedure = storeProcedure;
        }
        public async Task<UserInfoDto> RegisterAsync(string userName, string nickname, string pwd)
        {
            var user = await UserRepository.GetByUserNameAsync(userName);

            if (user != null)
            {
                throw new Exception("username is exist.");
            }

            user = new User
            {
                f_userName = userName,
                f_nickname = nickname,
                f_pwd = EncryptHelper.Encrypt(pwd),
                f_level = 0
            };

            _ = await UserRepository.AddAsync(user);

            user = await UserRepository.GetByUserNameAsync(userName);

            await StoreProcedure.AddLoginRecord(user.f_id);

            user = await UserRepository.GetByUserNameAsync(userName);

            var jwt = await TokenService.GenerateJwtAsync(user.f_id, user.f_level);

            return new UserInfoDto
            {
                UserId = user.f_id,
                UserName = userName,
                Nickname = nickname,
                Token = jwt
            };
        }
    }
}
