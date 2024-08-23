 using System;
using Domain.Common;

namespace Domain.Models
{
	public class YachtImage : BaseEntity
	{
		public string Image { get; set; }
		public bool IsMain { get; set; } = false;
		public int YachtId { get; set; }
		public Yacht Yacht { get; set; }
	}
}

