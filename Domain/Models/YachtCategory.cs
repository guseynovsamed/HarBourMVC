using System;
using Domain.Common;

namespace Domain.Models
{
	public class YachtCategory : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string IconImage { get; set; }
		public ICollection<Yacht> Yachts { get; set; }
	}
}

