using System;
using HarBourBackEnd.ViewModels.Yacht;

namespace HarBourBackEnd.ViewModels.WaterSport
{
	public class WaterSportDetailPageVM
	{
        public WaterSportDetailVM WaterSportDetail { get; set; }
        public IEnumerable<Domain.Models.WaterSport> WaterSports { get; set; }
    }
}

