using System;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public interface IBlogRepository : IBaseRepository<Blog>
	{
        Task<Blog> GetBlogByIdWithComment(int id);
        Task<IEnumerable<Blog>> GetGetAllWithIncludes();
    }
}

