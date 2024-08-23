using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IAboutRepository : IBaseRepository<About>
    {
        Task<About> GetByIdWithImages(int id);
        Task<IEnumerable<About>> GetAllWithImages();
        Task DeleteImage(AboutImage image);
        Task ChangeMainImage(int aboutId, int id);
    }
}

