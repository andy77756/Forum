using ForumLib.Models;
using System.Threading.Tasks;

namespace ForumLib.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> IsExist(string account);
    }
}
