using System;
using Repository.Data;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class SliderVideoRepository : BaseRepository<SliderVideo> , ISliderVideoRepository
	{
		public SliderVideoRepository(AppDbContext context) : base(context) { }
	}
}

