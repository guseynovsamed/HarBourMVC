using System;
using Domain.Models;

namespace HarBourBackEnd.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<SliderVideo> SliderVideos { get; set; }
		public IEnumerable<Subscriber> Subscribers { get; set; }
		public IEnumerable<Domain.Models.Yacht> Yachts { get; set; }
		public IEnumerable<YachtCategory> YachtCategories { get; set; }
		public IEnumerable<Domain.Models.WaterSport> WaterSports { get; set; }
		public IEnumerable<Domain.Models.DestinationCity> Cities { get; set; }
		public List<Domain.Models.Comment> Comments { get; set; }
		public IEnumerable<Domain.Models.Blog> Blogs { get; set; }
	}
}

