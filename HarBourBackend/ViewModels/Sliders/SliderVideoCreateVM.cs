using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.Sliders
{
	public class SliderVideoCreateVM
	{
        [Required]
        public string TopTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Video { get; set; }
    }
}

