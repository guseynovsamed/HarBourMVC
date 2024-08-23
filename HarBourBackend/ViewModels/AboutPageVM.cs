using System;
using Domain.Models;


namespace HarBourBackEnd.ViewModels
{
	public class AboutPageVM
	{
		public Domain.Models.About About { get; set; }
		public List<Domain.Models.Comment> Comment { get; set; }
		public IEnumerable<Domain.Models.Partner> Partner { get; set; }
		public IEnumerable<Domain.Models.Staff> Staff { get; set; }
    }
}

