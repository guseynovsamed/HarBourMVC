using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.YacthCategory
{
	public class YachtCategoryEditVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? IconImage { get; set; }
        public IFormFile? NewIconImage { get; set; }
    }
}

