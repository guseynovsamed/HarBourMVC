using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.Staff
{
	public class StaffEditVM
	{
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string Awards { get; set; }
        [Required]
        public int PositionId { get; set; }
        public  string? Image { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}

