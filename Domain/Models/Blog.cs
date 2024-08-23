 using System;
using Domain.Common;

namespace Domain.Models
{
	public class Blog : BaseEntity
	{
		public string Title { get; set; }
		public string Image { get; set; }
		public string Content { get; set; }
		public string Description { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public string BackgroundImage { get; set; }
	}
}

