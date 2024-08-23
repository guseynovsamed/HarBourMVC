using System;
using HarBourBackEnd.ViewModels.DestinationCity;

namespace HarBourBackEnd.ViewModels.Staff
{
	public class StaffDetailPageVM
	{
        public StaffDetailVM StaffDetail { get; set; }
        public IEnumerable<Domain.Models.Staff> Staff { get; set; }
    }
}

