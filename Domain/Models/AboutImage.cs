using System;
using Domain.Common;

namespace Domain.Models
{
	public class AboutImage : BaseEntity
	{
		public string Image { get; set; }
		public int AboutId { get; set; }
		public About About { get; set; }
		public bool IsMain { get; set; } = false;
	}
}

