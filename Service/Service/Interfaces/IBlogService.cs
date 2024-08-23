using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IBlogService
	{
        Task Create(Blog blog);
        Task<IEnumerable<Blog>> GetAll();
        Task Delete(Blog blog);
        Task<Blog> GetById(int id);
        Task Edit(int id, Blog blog);
    }
}

