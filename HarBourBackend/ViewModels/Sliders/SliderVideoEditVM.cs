using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.Sliders
{
	public class SliderVideoEditVM
	{
        [Required]
        public string TopTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ExistVideo { get; set; }
        public IFormFile? NewVideo { get; set; }
    }
}

