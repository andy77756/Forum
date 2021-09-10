using ForumLib.Dtos;
using ForumLib.Models;
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
        public Task<Result<UserInfoDto>> LoginAsync(string userName, string pwd);

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public Task<Result> LogoutAsync();

    }
}
