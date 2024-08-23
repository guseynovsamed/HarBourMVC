using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.Blog
{
	public class BlogDetailVM
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string BackgroundImage { get; set; }
        public IEnumerable<Domain.Models.Comment> Comments { get; set; }
    }
}

