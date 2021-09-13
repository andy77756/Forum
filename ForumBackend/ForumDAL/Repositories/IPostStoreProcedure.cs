using ForumDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public interface IPostStoreProcedure
    {
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="topic">標題</param>
        /// <param name="content">文章內容</param>
        /// <returns></returns>
        public Task<QueryResult<PostDto>> AddPostAsync(int userId, string topic, string content);

        /// <summary>
        /// 取得所有文章列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PostDto>> GetPostsAsync();

        /// <summary>
        /// 取得篩選後文章列表
        /// </summary>
        /// <param name="key">關鍵字</param>
        /// <returns></returns>
        public Task<IEnumerable<PostDto>> GetPostsAsync(string key);

        /// <summary>
        /// 取得文章回覆內容
        /// </summary>
        /// <param name="postId">文章id</param>
        /// <returns></returns>
        public Task<IEnumerable<ReplyDto>> GetRepliesByPostIdAsync(int postId);
    }
}
