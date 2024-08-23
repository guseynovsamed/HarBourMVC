using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class YachtCategoryRepository : BaseRepository<YachtCategory> , IYachtCategoryRepository
    {
		public YachtCategoryRepository(AppDbContext context) : base(context)
		{
		}
	}
}

