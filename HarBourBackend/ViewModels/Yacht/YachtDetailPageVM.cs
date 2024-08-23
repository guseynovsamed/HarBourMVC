using System;
using Domain.Models;
using HarBourBackEnd.ViewModels.Reservation;

namespace HarBourBackEnd.ViewModels.Yacht
{
	public class YachtDetailPageVM
	{
		public int Id { get; set; }
		public YachtDetailVM YachtDetail { get; set; }
		public IEnumerable<Domain.Models.Yacht> Yachts { get; set; }
		public ReservationCreateVM RezCreate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Domain.Models.Yacht Yacht { get; set; }
		public List<ReservDatesVM> ReservDates { get; set; }
    }
}

