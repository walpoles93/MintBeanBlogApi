using Microsoft.EntityFrameworkCore;
using MintBeanBlogApi.Models;

namespace MintBeanBlogApi.Persistence
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
