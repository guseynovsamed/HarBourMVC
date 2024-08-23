using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class SubscriberRepository : BaseRepository<Subscriber> , ISubscriberRepository
    {
		public SubscriberRepository(AppDbContext context) : base(context) { }
	}
}

