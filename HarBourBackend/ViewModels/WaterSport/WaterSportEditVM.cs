using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.WaterSport
{
	public class WaterSportEditVM
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Policy { get; set; }
        public decimal Price { get; set; }
        public string? ExistBackgroundImage { get; set; }
        public IFormFile? NewBackgroundImage { get; set; }
        public List<WaterSportImage>? ExistSportImages { get; set; }
        public List<IFormFile>? NewSportImages { get; set; }
    }
}

