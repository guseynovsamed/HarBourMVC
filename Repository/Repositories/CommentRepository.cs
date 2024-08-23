using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class CommentRepository : BaseRepository<Comment> , ICommentRepository 
    {
		public CommentRepository(AppDbContext context) : base(context)
		{
		}

        public async Task<List<Comment>> GetCommentByBlog(int id)
        {
            return await _context.Comments.Where(m => m.BlogId == id).Include(m => m.User).ToListAsync();
        }

        public async Task<List<Comment>> GetAllWithIncludes()
        {
            return await _context.Comments.Include(m => m.User).Include(m => m.Blogs).ToListAsync();
        }
    }
}

