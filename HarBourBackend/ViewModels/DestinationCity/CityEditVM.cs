using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.DestinationCity
{
	public class CityEditVM
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
        public int CountryId { get; set; }

        public string? ExistBackgroundImage { get; set; }
        public IFormFile? NewBackgroundImage { get; set; }


        public ICollection<DestinationImage>? ExistDestinationImages { get; set; }
        public ICollection<IFormFile>? NewDestinationImages { get; set; }
    }
}

