using ForumDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IAuthorizeStoreProcedure
    {
        public Task AddLoginRecord(int userId);

        public Task<QueryResult<User>> RegisterAsync(string userName, string nickname, string pwd);

        public Task<QueryResult<User>> LoginAsync(string userName, string pwd);

        public Task<IEnumerable<PostDto>> GetPostsAsync();

        public Task<IEnumerable<PostDto>> GetPostsAsync(string key);

        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int postId);
    }
}
