using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
	public class AppUser : IdentityUser
    {
		public string FullName { get; set; }
		public ICollection<Comment> Comments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}

