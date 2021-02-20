using MintBeanBlogApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MintBeanBlogApi.Persistence
{
    public static class ApiDbContextSeed
    {
        public static async Task SeedData(ApiDbContext dbContext)
        {
            // seed posts
            var posts = new List<Post>
            {
                new Post { Title = "Test Post 1", Content = "This is a test post", Published = true },
                new Post { Title = "Test Post 2", Content = "This is a test post too", Published = false }
            };
            dbContext.AddRange(posts);

            await dbContext.SaveChangesAsync();
        }
    }
}
