using System;
using Domain.Common;

namespace Domain.Models
{
    public class Comment : BaseEntity
    {
        public string CommentText { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int BlogId { get; set; }
        public Blog Blogs { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}

