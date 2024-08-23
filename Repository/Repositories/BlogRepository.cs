using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
		public BlogRepository(AppDbContext context) : base(context) { }

        public async Task<Blog> GetBlogByIdWithComment(int id)
        {
            return await _entities.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Blog>> GetGetAllWithIncludes()
        {
            return await _entities.Include(m => m.Comments).ThenInclude(m=>m.User).ToListAsync(); 
        }
    }
}

