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
        public Task<QueryResult<Post>> AddPostAsync(int userId, string topic, string content);

        /// <summary>
        /// 取得所有文章列表, 預設page index=1, page size = 10
        /// </summary>
        /// <param name="pageIndex">頁數</param>
        /// <param name="pageSize">每頁數量</param>
        /// <returns></returns>
        public Task<QueryResult<IEnumerable<Post>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null);

        /// <summary>
        /// 取得篩選後文章列表
        /// </summary>
        /// <param name="key">關鍵字</param>
        /// <returns></returns>
        public Task<QueryResult<IEnumerable<Post>>> GetPostsAsync(string key, int? pageIndex = null, int? pageSize = null);

        /// <summary>
        /// 取得文章回覆內容
        /// </summary>
        /// <param name="postId">文章id</param>
        /// <returns></returns>
        public Task<QueryResult<Replys>> GetRepliesByPostIdAsync(int postId, int? pageIndex = null, int? pageSize = null);

        /// <summary>
        /// 新增回覆
        /// </summary>
        /// <param name="postId">文章id</param>
        /// <param name="userId">發文者userId</param>
        /// <param name="content">回覆內容</param>
        /// <returns></returns>
        public Task<QueryResult<Reply>> AddReplyAsync(int postId, int userId, string content);
    }
}
