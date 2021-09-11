using ForumDAL.Models;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IPostStoreProcedure
    {
        public Task<QueryResult<PostDto>> AddPostAsync(int userId, string topic, string content);
    }
}
