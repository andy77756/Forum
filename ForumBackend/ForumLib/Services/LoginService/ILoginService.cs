using ForumLib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Services.LoginService
{
    public interface ILoginService
    {
        /// <summary>
        /// login
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="pwd">password</param>
        /// <returns></returns>
        public Task<UserInfoDto> LoginAsync(string userName, string pwd);

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public Task<bool> LogoutAsync();

    }
}
