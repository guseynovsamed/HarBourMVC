using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class StaffRepository : BaseRepository<Staff> ,IStaffRepository
	{
		public StaffRepository(AppDbContext context) : base(context)
		{

		}

        public async Task<IEnumerable<Staff>> GetAllWithInclude()
        {
            return await _entities.Include(m => m.Position).ToListAsync();
        }

        public async Task<Staff> GetByIdWithPositions(int id)
        {
            return await _entities.Include(m => m.Position).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

