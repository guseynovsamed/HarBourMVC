using System;
using Domain.Common;

namespace Domain.Models
{
	public class DestinationImage : BaseEntity
	{
		public string Image { get; set; }
		public bool IsMain { get; set; } = false;
		public int DestinationId { get; set; }
		public DestinationCity DestinationCity { get; set; }
	}
}

