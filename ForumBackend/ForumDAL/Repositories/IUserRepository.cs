using ForumDAL.Models;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> IsExist(string account);
    }
}
