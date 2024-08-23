using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class DestinationCountryRepository : BaseRepository<DestinationCountry> , IDestinationCountryRepository
    {
		public DestinationCountryRepository(AppDbContext context) : base(context) { }

	}
}

