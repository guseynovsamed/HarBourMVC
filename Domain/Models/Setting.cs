using System;
using Domain.Common;

namespace Domain.Models
{
	public class Setting : BaseEntity
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}

