using System;
using Domain.Common;

namespace Domain.Models
{
	public class WaterSport : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Information { get; set; }
        public string Policy { get; set; }
        public decimal Price { get; set; }
		public string BackgroundImage { get; set; }
		public List<WaterSportImage> SportImages { get; set; }
	}
}

