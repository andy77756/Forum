using ForumDAL.Models;
using ForumDAL.Repositories;
using ForumLib.Services.ForumService;
using ForumWebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService ForumService;
        private readonly IPostRepository PostRepository;
        private readonly IReplyRepository ReplyRepository;

        public ForumController(
            IForumService forumService, 
            IPostRepository postRepository,
            IReplyRepository replyRepository)
        {
            ForumService = forumService;
            PostRepository = postRepository;
            ReplyRepository = replyRepository;
        }

        [HttpGet]
        [Route("Posts")]
        public async Task<IEnumerable<PostDto>> GetPostsAsync(string key)
        {
            if (!String.IsNullOrWhiteSpace(key))
            {
                return await ForumService.GetPostsByFilterAsync(key);
            }
            return await ForumService.GetPostsAsync();
        }

        [HttpGet]
        [Route("Replies")]
        public async Task<IEnumerable<ReplyDto>> GetRepliesAsync(int postId)
        {
            return await ForumService.GetRepliesByPostIdAsync(postId);
        }

        [TypeFilter(typeof(LevelTwoAuthorizationFilter))]
        [HttpPost]
        [Route("Posts")]
        public async Task<IActionResult> AddPostAsync(Post post)
        {
            try
            {
                await PostRepository.AddAsync(post);
                return Created("", null);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [TypeFilter(typeof(LevelTwoAuthorizationFilter))]
        [HttpPut]
        [Route("Posts")]
        public async Task<IActionResult> UpdatePostAsync(Post post)
        {
            post.f_updateAt = DateTime.Now;
            try
            {
                await PostRepository.UpdateAsync(post);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [TypeFilter(typeof(LevelOneAuthorizationFilter))]
        [HttpPost]
        [Route("Replies")]
        public async Task<IActionResult> AddReplyAsync(Reply reply)
        {
            try
            {
                await ReplyRepository.AddAsync(reply);
                return Created("", null);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [TypeFilter(typeof(LevelOneAuthorizationFilter))]
        [HttpPut]
        [Route("Replies")]
        public async Task<IActionResult> UpdateReplyAsync(Reply reply)
        {
            reply.f_updateAt = DateTime.Now;
            try
            {
                await ReplyRepository.UpdateAsync(reply);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
