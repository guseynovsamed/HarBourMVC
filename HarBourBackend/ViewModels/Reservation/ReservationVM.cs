using System;
using Domain.Helpers.Enums;

namespace HarBourBackEnd.ViewModels.Reservation
{
	public class ReservationVM
	{
		public int Id { get; set; }
		public string UserEmail { get; set; }
		public string YachtName { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}

