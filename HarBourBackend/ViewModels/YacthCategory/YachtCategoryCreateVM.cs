using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.YacthCategory
{
	public class YachtCategoryCreateVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile IconImage { get; set; }
    }
}

