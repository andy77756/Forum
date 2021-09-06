using ForumLib.Dtos;
using System.Threading.Tasks;

namespace ForumLib.Services.RegisterService
{
    public interface IRegisterService
    {
        public Task<UserInfoDto> RegisterAsync(string userName, string nickname, string pwd);
    }
}
