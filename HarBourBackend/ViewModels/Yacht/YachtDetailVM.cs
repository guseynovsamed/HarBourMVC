using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.Yacht
{
	public class YachtDetailVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Guest { get; set; }
        public decimal Price { get; set; }
        public string Build { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public ICollection<YachtImage> YachtImages { get; set; }
        public string YachtCategory { get; set; } 
    }
}

