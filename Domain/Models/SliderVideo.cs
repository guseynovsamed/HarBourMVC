using System;
using Domain.Common;

namespace Domain.Models
{
	public class SliderVideo : BaseEntity
	{
		public string TopTitle { get; set; }
		public string MainTitle { get; set; }
		public string Description { get; set; }
		public string Video { get; set; }
    }
}

