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

        public async Task<Result> AddPostAsync(int userId, string topic, string content)
        {
            var result = await PostStoreProcedure.AddPostAsync(userId, topic, content);
            return new Result(result.StatusCode);
        }

        public async Task<Result<ReplyDto>> AddReplyAsync(int postId, int userId, string content)
        {
            var result = await PostStoreProcedure.AddReplyAsync(postId, userId, content);

            return new Result<ReplyDto>(
                    result.StatusCode,
                    new ReplyDto
                    {
                        Id = result.Result.Id,
                        UserName = result.Result.UserName,
                        Nickname = result.Result.Nickname,
                        Content = result.Result.Content,
                        CreateAt = result.Result.CreateAt.ToString("yyyy-MM-dd HH:mm"),
                        UpdateAt = result.Result.UpdateAt?.ToString("yyyy-MM-dd HH:mm")
                    }
                );           
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
                    ReplyAt = post.ReplyAt?.ToString("yyyy-MM-dd HH:mm"),
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
                    ReplyAt = post.ReplyAt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyCount = post.ReplyCount
                })
            );
        }

        public async Task<Result<ReplysDto>> GetRepliesByPostIdAsync(int id, int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetRepliesByPostIdAsync(id, pageIndex, pageSize);

            return new Result<ReplysDto>(
                result.StatusCode, 
                new ReplysDto 
                { 
                    Post = new PostDto
                    {
                        Topic = result.Result.Post.Topic,
                        Content = result.Result.Post.Content,
                        Postid = result.Result.Post.Postid,
                        PostUserName = result.Result.Post.PostUserName,
                        PostAt = result.Result.Post.PostAt?.ToString("yyyy-MM-dd HH:mm"),
                        ReplyUserName = result.Result.Post.ReplyUserName,
                        ReplyAt = result.Result.Post.ReplyAt?.ToString("yyyy-MM-dd HH:mm"),
                        ReplyCount = result.Result.Post.ReplyCount
                    },
                    Replies = result.Result.Replies.Select(reply => new ReplyDto
                    {
                        Id = reply.Id,
                        Content = reply.Content,
                        UserName = reply.UserName,
                        Nickname = reply.Nickname,
                        CreateAt = reply.CreateAt.ToString("yyyy-MM-dd HH:mm"),
                        UpdateAt = reply.UpdateAt?.ToString("yyyy-MM-dd HH:mm")
                    })
                }
            );
        }
    }
}
