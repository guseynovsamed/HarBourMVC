using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<List<Comment>> GetCommentByBlog(int id);
        Task<List<Comment>> GetAllWithIncludes();
    }
}

