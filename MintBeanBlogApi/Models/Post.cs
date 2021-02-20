using System;

namespace MintBeanBlogApi.Models
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
