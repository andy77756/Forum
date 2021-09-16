using ForumDAL.Models;
using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLib.Services.ForumService
{
    public class ForumService : IForumService
    {
        private readonly IPostStoreProcedure PostStoreProcedure;

        public ForumService(
            IPostStoreProcedure postStoreProcedure
            )
        {
            PostStoreProcedure = postStoreProcedure;
        }

        public async Task<Result<Post>> AddPostAsync(int userId, string topic, string content)
        {
            var result = await PostStoreProcedure.AddPostAsync(userId, topic, content);
            return new Result<Post>(result.StatusCode);
        }

        public async Task<Result<IEnumerable<PostDto>>> GetPostsAsync(int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetPostsAsync(pageIndex, pageSize);
            
            return new Result<IEnumerable<PostDto>>(
                result.StatusCode, 
                result.Result.Select(post => new PostDto
                {
                    Postid = post.Postid,
                    Topic = post.Topic,
                    PostUserName = post.PostUserName,
                    PostAt = post.PostAt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyUserName = post.ReplyUserName,
                    ReplyDt = post.ReplyDt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyCount = post.ReplyCount
                })
            );
        }

        public async Task<Result<IEnumerable<PostDto>>> GetPostsByFilterAsync(string key, int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetPostsAsync(key, pageIndex, pageSize);

            return new Result<IEnumerable<PostDto>>(
                result.StatusCode,
                result.Result.Select(post => new PostDto
                {
                    Postid = post.Postid,
                    Topic = post.Topic,
                    PostUserName = post.PostUserName,
                    PostAt = post.PostAt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyUserName = post.ReplyUserName,
                    ReplyDt = post.ReplyDt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyCount = post.ReplyCount
                })
            );
        }

        public async Task<Result<Replys>> GetRepliesByPostIdAsync(int id, int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetRepliesByPostIdAsync(id, pageIndex, pageSize);

            return new Result<Replys>(result.StatusCode, result.Result);
        }
    }
}
