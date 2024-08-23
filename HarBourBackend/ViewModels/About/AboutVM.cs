using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels.About
{
	public class AboutVM
	{
        public int Id { get; set; }
        public string SinceDay { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Advantage1 { get; set; }
        public string Advantage2 { get; set; }
        public string Advantage3 { get; set; }
        public ICollection<AboutImage> AboutImages { get; set; }
    }
}

