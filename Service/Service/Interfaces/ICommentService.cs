using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface ICommentService
	{
        Task<List<Comment>> GetAll();
        Task Create(Comment comment);
        Task<List<Comment>> GetCommentsByBlog(int id);
        Task<Comment> GetById(int id);
        Task Delete(Comment comment);
    }
}

