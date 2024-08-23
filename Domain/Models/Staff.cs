using System;
using Domain.Common;

namespace Domain.Models
{
	public class Staff : BaseEntity
	{
		public string Fullname { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		public string Mail { get; set; }
		public string Biography { get; set; }
		public string Education { get; set; }
		public string Awards { get; set; }
		public int PositionId { get; set; }
		public Position Position { get; set; }
		public string Image { get; set; }
	}
}

