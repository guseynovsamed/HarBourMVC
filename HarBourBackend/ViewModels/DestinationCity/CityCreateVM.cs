using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.DestinationCity
{
	public class CityCreateVM
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int AccommodationDay { get; set; }
        [Required]
        public int MinLimit { get; set; }
        [Required]
        public int MaxLimit { get; set; }
        [Required]
        public string Departure { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<IFormFile>? DestinationImages { get; set; }
        public int CountryId { get; set; }
        [Required]
        public IFormFile? BackgroundImage { get; set; }
    }
}

