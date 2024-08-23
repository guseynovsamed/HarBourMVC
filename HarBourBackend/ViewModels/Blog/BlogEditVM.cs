using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.Blog
{
	public class BlogEditVM
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ExistImage { get; set; }
        public IFormFile? Image { get; set; }
        public string ExistBackgroundImage { get; set; }
        public IFormFile? BackgroundImage { get; set; }
    }
}

