using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extensions;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class DestinationCityRepository : BaseRepository<DestinationCity>, IDestinationCityRepository
    {
        public DestinationCityRepository(AppDbContext context) : base(context)
        {

        }


        public async Task ChangeMainImage(int cityId, int id)
        {
            var existCity = await _entities.Include(m => m.DestinationImages).FirstOrDefaultAsync(m => m.Id == cityId);
            foreach (var item in existCity.DestinationImages)
            {
                item.IsMain = false;
            }

            existCity.DestinationImages.FirstOrDefault(m => m.Id == id).IsMain = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImage(DestinationImage image)
        {
            _context.DestinationImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DestinationCity>> GetAllPaginatedDatas(int page, int take = 4)
        {
            return await _entities.Include(m => m.Country).Include(m => m.DestinationImages).Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<DestinationCity>> GetAllWithImages()
        {
            return await _entities.IncludeMultiple<DestinationCity>(m => m.DestinationImages).ToListAsync();

        }

        public async Task<IEnumerable<DestinationCity>> GetAllWithIncludes()
        {
            return await _entities.Include(m => m.Country).Include(m=>m.DestinationImages).ToListAsync();
        }

        public async Task<DestinationCity> GetByIdWithCountry(int id)
        {
            return await _entities.Include(m => m.Country).Include(m => m.DestinationImages).FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<int> GetCount()
        {
            return await _context.DestinationCities.CountAsync();
        }
    }
}

