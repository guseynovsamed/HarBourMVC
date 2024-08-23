using System;
using Domain.Common;

namespace Domain.Models
{
	public class DestinationCountry : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<DestinationCity> Cities { get; set; }
	}
}

