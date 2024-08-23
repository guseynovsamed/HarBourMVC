using System;
using Domain.Common;

namespace Domain.Models
{
	public class Position : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Staff> Staff { get; set; }
	}
}

