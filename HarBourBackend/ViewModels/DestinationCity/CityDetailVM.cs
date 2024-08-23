using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.DestinationCity
{
	public class CityDetailVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AccommodationDay { get; set; }
        public int MinLimit { get; set; }
        public int MaxLimit { get; set; }
        public string Departure { get; set; }
        public string Description { get; set; }
        public ICollection<DestinationImage> DestinationImages { get; set; }
        public string Country { get; set; }
        public string? BackgroundImage { get; set; }
    }
}

