using ForumLib.Enums;
using ForumLib.Extensions;
using ForumLib.Models;
using ForumLib.Services.ForumService;
using ForumWebApi.Filters;
using ForumWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForumWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService ForumService;

        public ForumController(
            IForumService forumService)
        {
            ForumService = forumService;
        }

        [TypeFilter(typeof(LevelTwoAuthorizationFilter))]
        [HttpPost]
        [Route("Posts")]
        public async Task<IActionResult> AddPostAsync(PostFrontend post)
        {
            if (
                string.IsNullOrEmpty(post.Topic) ||
                post.Topic.Length < 3 ||
                post.Topic.Length > 30)
            {
                return Ok(new Result((int)StatusCodeEnum.TopicFormatInvalid));
            }

            if (
                string.IsNullOrEmpty(post.Content) ||
                post.Content.Length < 10)
            {
                return Ok(new Result((int)StatusCodeEnum.ContentFormatInvalid));
            }

            var result = await ForumService.AddPostAsync(HttpContext.User.GetID(), post.Topic, post.Content);
            return Ok(result);
        }

        /// <summary>
        /// 取得文章列表
        /// </summary>
        /// <param name="keyTopic">關鍵字</param>
        /// <param name="pageIndex">頁數</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Posts")]
        public async Task<IActionResult> GetPost(string keyTopic, string keyNickname, int? pageIndex, int? pageSize)
        {
            return Ok(await ForumService.GetPostsAsync(keyTopic, keyNickname, pageIndex: pageIndex, pageSize: pageSize));
        }

        [HttpGet]
        [Route("Reply")]
        public async Task<IActionResult> GetReplies(int postId, int? pageIndex, int? pageSize)
        {
            return Ok(await ForumService.GetRepliesByPostIdAsync(postId, pageIndex: pageIndex, pageSize: pageSize));
        }

        [TypeFilter(typeof(LevelOneAuthorizationFilter))]
        [HttpPost]
        [Route("Reply")]
        public async Task<IActionResult> AddReplyAsync(ReplyFrontend reply)
        {
            if (string.IsNullOrWhiteSpace(reply.Content))
            {
                return Ok(new Result((int)StatusCodeEnum.ReplyContentInvalid));
            }
            return Ok(await ForumService.AddReplyAsync(reply.PostId, reply.UserId, reply.Content));
        }
    }
}
