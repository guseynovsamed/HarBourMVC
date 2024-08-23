using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class AboutRepository : BaseRepository<About> , IAboutRepository
    {
		public AboutRepository(AppDbContext context) : base(context) { }


        public async Task<IEnumerable<About>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<About>(m => m.AboutImages).ToListAsync();
        }



        public async Task<About> GetByIdWithImages(int id)
        {
            return await _entities.Include(m => m.AboutImages).FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task DeleteImage(AboutImage image)
        {
            _context.AboutImages.Remove(image);
            await _context.SaveChangesAsync();
        }



        public async Task ChangeMainImage(int aboutId, int id)
        {
            var existAbout = await _entities.Include(m => m.AboutImages).FirstOrDefaultAsync(m => m.Id == aboutId);
            foreach (var item in existAbout.AboutImages)
            {
                item.IsMain = false;
            }

            existAbout.AboutImages.FirstOrDefault(m => m.Id == id).IsMain = true;
            await _context.SaveChangesAsync();
        }
    }
}

