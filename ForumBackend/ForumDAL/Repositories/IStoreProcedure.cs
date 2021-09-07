using ForumDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IStoreProcedure
    {
        public Task AddLoginRecord(int userId);

        public Task<IEnumerable<PostDto>> GetPostsAsync();

        public Task<IEnumerable<PostDto>> GetPostsAsync(string key);

        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int postId);
    }
}
