using ForumLib.Dtos;
using ForumLib.Models;
using System.Threading.Tasks;

namespace ForumLib.Services.RegisterService
{
    public interface IRegisterService
    {
        public Task<Result<UserInfoDto>> RegisterAsync(string userName, string nickname, string pwd);
    }
}
