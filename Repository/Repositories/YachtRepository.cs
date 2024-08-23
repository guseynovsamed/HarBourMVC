using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class YachtRepository : BaseRepository<Yacht>, IYachtRepository
    {
        public YachtRepository(AppDbContext context) : base(context)
        {

        }



        public async Task ChangeMainImage(int yachtId, int id)
        {
            var existYacht = await _entities.Include(m => m.YachtImages).FirstOrDefaultAsync(m => m.Id == yachtId);
            foreach (var item in existYacht.YachtImages)
            {
                item.IsMain = false;
            }
            existYacht.YachtImages.FirstOrDefault(m => m.Id == id).IsMain = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(YachtImage image)
        {
            _context.YachtImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Yacht>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<Yacht>(m => m.YachtImages).ToListAsync();
        }

        public async Task<IEnumerable<Yacht>> GetAllWithIncludes()
        {
            return await _entities.Include(m => m.YachtCategory).Include(m=>m.YachtImages).ToListAsync();
        }

        public async Task<Yacht> GetByIdWithCategory(int id)
        {
            return await _entities.Include(m => m.YachtCategory).Include(m => m.YachtImages).Include(m=>m.Reservations).ThenInclude(m=>m.AppUser).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

