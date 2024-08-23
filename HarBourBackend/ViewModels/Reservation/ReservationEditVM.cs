using System;
using Domain.Helpers.Enums;

namespace HarBourBackEnd.ViewModels.Reservation
{
	public class ReservationEditVM
	{
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

