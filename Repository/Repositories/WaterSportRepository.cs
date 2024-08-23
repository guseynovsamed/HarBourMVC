using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.Repositories.Interfaces
{
	public class WaterSportRepository : BaseRepository<WaterSport>, IWaterSportRepository
    {
		public WaterSportRepository(AppDbContext context) : base(context)
		{

		}

        public async Task ChangeMainImage(int sportId, int id)
        {
            var existAbout = await _entities.Include(m => m.SportImages).FirstOrDefaultAsync(m => m.Id == sportId);
            foreach (var item in existAbout.SportImages)
            {
                item.IsMain = false;
            }

            existAbout.SportImages.FirstOrDefault(m => m.Id == id).IsMain = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(WaterSportImage image)
        {
            _context.WaterSportImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WaterSport>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<WaterSport>(m => m.SportImages).ToListAsync();

        }

        public async Task<IEnumerable<WaterSport>> GetAllWithIncludes()
        {
            return await _entities.Include(m => m.SportImages).ToListAsync();
        }

        public async Task<WaterSport> GetByIdWithImages(int id)
        {
            return await _entities.Include(m => m.SportImages).FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}

