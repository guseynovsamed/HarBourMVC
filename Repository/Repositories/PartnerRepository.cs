using System;
using Domain.Models;
using Repository.Data;

namespace Repository.Repositories.Interfaces
{
	public class PartnerRepository : BaseRepository<Partner> , IPartnerRepository
	{
		public PartnerRepository(AppDbContext context) : base(context) { }

	}
}

