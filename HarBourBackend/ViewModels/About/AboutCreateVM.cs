using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.About
{
	public class AboutCreateVM
	{
        [Required]
        public string SinceDay { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Advantage1 { get; set; }
        [Required]
        public string Advantage2 { get; set; }
        [Required]
        public string Advantage3 { get; set; }
        [Required]
        public List<IFormFile> AboutImages { get; set; }
    }
}

