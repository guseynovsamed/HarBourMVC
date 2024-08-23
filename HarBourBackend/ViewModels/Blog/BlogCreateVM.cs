using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.Blog
{
	public class BlogCreateVM
	{
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public IFormFile BackgroundImage { get; set; }
    }
}

