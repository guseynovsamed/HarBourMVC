using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IYachtRepository : IBaseRepository<Yacht>
	{
        Task<IEnumerable<Yacht>> GetAllWithImages();
        Task<IEnumerable<Yacht>> GetAllWithIncludes();
        Task DeleteImage(YachtImage image);
        Task ChangeMainImage(int yachtId, int id);
        Task<Yacht> GetByIdWithCategory(int id);
    }
}

