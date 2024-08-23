using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.WaterSport
{
	public class WaterSportVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Policy { get; set; }
        public decimal Price { get; set; }
        public string BackgroundImage { get; set; }
        public List<WaterSportImage> SportImages { get; set; }
    }
}

