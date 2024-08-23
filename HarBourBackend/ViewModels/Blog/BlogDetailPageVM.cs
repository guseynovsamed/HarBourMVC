using System;
using HarBourBackEnd.ViewModels.Comment;

namespace HarBourBackEnd.ViewModels.Blog
{
    public class BlogDetailPageVM
    {
        public BlogDetailVM Blog { get; set; }
        public IEnumerable<Domain.Models.Blog> Blogs { get; set; }
        public CommentVM CommentData { get; set; }
        public IEnumerable<Domain.Models.Comment> BlogComments { get; set; }
    }
}

