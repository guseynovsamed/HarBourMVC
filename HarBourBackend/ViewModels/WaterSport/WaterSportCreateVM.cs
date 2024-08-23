using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.WaterSport
{
	public class WaterSportCreateVM
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Policy { get; set; }
        public decimal Price { get; set; }
        public IFormFile BackgroundImage { get; set; }
        public List<IFormFile> SportImages { get; set; }
    }
}

