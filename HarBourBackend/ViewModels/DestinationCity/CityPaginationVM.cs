using System;
using Domain.Models;
using HarBourBackEnd.Helpers;

namespace HarBourBackEnd.ViewModels.DestinationCity
{
	public class CityPaginationVM
	{
		public IEnumerable<CityVM> CityVM { get; set; }
        public Paginate<Domain.Models.DestinationCity> Pagination { get; set; }
    }
}

