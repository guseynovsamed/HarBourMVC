using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.Yacht
{
    public class YachtEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Length { get; set; }
        [Required]
        public int Guest { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Build { get; set; }
        [Required]
        public string Information { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Description { get; set; }

        public List<YachtImage>? YachtImages { get; set; }
        public List<IFormFile>? NewYachtImages { get; set; }
    }
}

