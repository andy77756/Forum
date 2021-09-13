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
    }
}
