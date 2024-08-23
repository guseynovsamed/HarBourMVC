using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IWaterSportRepository : IBaseRepository<WaterSport>
    {
        Task<WaterSport> GetByIdWithImages(int id);
        Task<IEnumerable<WaterSport>> GetAllWithImages();
        Task DeleteImage(WaterSportImage image);
        Task ChangeMainImage(int sportId, int id);
        Task<IEnumerable<WaterSport>> GetAllWithIncludes();
    }
}

