using System;
using Domain.Common;

namespace Domain.Models
{
	public class WaterSportImage : BaseEntity
	{
		public string Image { get; set; }
		public bool IsMain { get; set; } = false;
		public int WaterSportId { get; set; }
		public WaterSport WaterSport { get; set; }
	}
}

