using System;
using Domain.Common;

namespace Domain.Models
{
	public class ComplaintSuggest : BaseEntity
	{
        public string UserFullName { get; set; }
        public string UserPhone { get; set; }
        public string Subject { get; set; }
        public string UserEmail { get; set; }
        public string UserSuggest { get; set; }
    }
}

