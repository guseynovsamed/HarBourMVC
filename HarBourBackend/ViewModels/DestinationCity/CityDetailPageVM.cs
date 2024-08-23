using System;
namespace HarBourBackEnd.ViewModels.DestinationCity
{
	public class CityDetailPageVM
	{
		public CityDetailVM CityDetail { get; set; }
		public IEnumerable<Domain.Models.DestinationCity> DestinationCities { get; set; }
    }
}

