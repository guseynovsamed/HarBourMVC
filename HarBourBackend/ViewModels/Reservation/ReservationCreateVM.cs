using System;
namespace HarBourBackEnd.ViewModels.Reservation
{
	public class ReservationCreateVM
	{
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int AppUserId { get; set; }
        public int Id { get; set; }
        public int Guest { get; set; }
    }
}

