using ForumDAL.Repositories;
using ForumLib.Dtos;
using ForumLib.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            var result = await PostStoreProcedure.AddPostAsync(userId, HttpUtility.HtmlEncode(topic), HttpUtility.HtmlEncode(content));
            return new Result(result.StatusCode);
        }

        public async Task<Result<ReplyDto>> AddReplyAsync(int postId, int userId, string content)
        {
            var result = await PostStoreProcedure.AddReplyAsync(postId, userId, HttpUtility.HtmlEncode(content));

            return new Result<ReplyDto>(
                    result.StatusCode,
                    new ReplyDto
                    {
                        Id = result.Result.Id,
                        UserName = result.Result.UserName,
                        Nickname = result.Result.Nickname,
                        Content = HttpUtility.HtmlDecode(result.Result.Content),
                        CreateAt = result.Result.CreateAt.ToString("yyyy-MM-dd HH:mm"),
                        UpdateAt = result.Result.UpdateAt?.ToString("yyyy-MM-dd HH:mm")
                    }
                );           
        }

        public async Task<Result<PostsDto>> GetPostsAsync(string keyTopic, string keyNickname,int? pageIndex = null, int? pageSize = null)
        {
            var result = await PostStoreProcedure.GetPostsAsync(keyTopic, keyNickname, pageIndex, pageSize);

            var returnData = new PostsDto
            {
                Posts = result.Result.Select(post => new PostDto
                {
                    Postid = post.Postid,
                    Topic = HttpUtility.HtmlDecode(post.Topic),
                    PostUserName = post.PostUserName,
                    PostAt = post.PostAt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyUserName = post.ReplyUserName,
                    ReplyAt = post.ReplyAt?.ToString("yyyy-MM-dd HH:mm"),
                    ReplyCount = post.ReplyCount
                }),
                MetaData = new MetaDataDto
                {
                    Length = result.Length ?? 0,
                    CurrentIndex = pageIndex ?? 0,
                    pageSize = pageSize ?? 10
                }
            };

            return new Result<PostsDto>(
                result.StatusCode,
                returnData
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
                        Topic = HttpUtility.HtmlDecode(result.Result.Post.Topic),
                        Content = HttpUtility.HtmlDecode(result.Result.Post.Content),
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
                        Content = HttpUtility.HtmlDecode(reply.Content),
                        UserName = reply.UserName,
                        Nickname = reply.Nickname,
                        CreateAt = reply.CreateAt.ToString("yyyy-MM-dd HH:mm"),
                        UpdateAt = reply.UpdateAt?.ToString("yyyy-MM-dd HH:mm")
                    }),
                    MetaData = new MetaDataDto
                    {
                        Length = result.Length??0,
                        CurrentIndex = pageIndex??0,
                        pageSize = pageSize??10
                    }
                }
            );
        }
    }
}
