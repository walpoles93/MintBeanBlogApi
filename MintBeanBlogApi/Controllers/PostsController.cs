using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MintBeanBlogApi.Models;
using MintBeanBlogApi.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MintBeanBlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public PostsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (post is null) return BadRequest();

            _dbContext.Add(post);
            await _dbContext.SaveChangesAsync();

            return new JsonResult(post.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool? published = null)
        {
            var postsQuery = _dbContext.Posts.AsQueryable();

            if (published.HasValue)
            {
                postsQuery = postsQuery.Where(p => p.Published == published.Value);
            }

            var posts = await postsQuery.ToListAsync();

            return new JsonResult(posts);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            return new JsonResult(post);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Post post)
        {
            if (post is null) return BadRequest();

            var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost is null) return BadRequest();

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.Published = post.Published;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete(Guid postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post is null) return BadRequest();

            _dbContext.Remove(post);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
