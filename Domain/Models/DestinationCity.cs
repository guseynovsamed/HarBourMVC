using System;
using Domain.Common;

namespace Domain.Models
{
	public class DestinationCity : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int AccommodationDay { get; set; }
		public int MinLimit { get; set; }
        public int MaxLimit { get; set; }
		public string Departure { get; set; }
        public string Description { get; set; }
        public ICollection<DestinationImage> DestinationImages { get; set; }
		public int CountryId { get; set; }
		public DestinationCountry Country { get; set; }
        public string? BackgroundImage { get; set; }
    }
}

