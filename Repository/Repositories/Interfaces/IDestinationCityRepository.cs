using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IDestinationCityRepository : IBaseRepository<DestinationCity>
    {
        Task<IEnumerable<DestinationCity>> GetAllWithImages();
        Task<IEnumerable<DestinationCity>> GetAllWithIncludes();
        Task DeleteImage(DestinationImage image);
        Task ChangeMainImage(int cityId, int id);
        Task<DestinationCity> GetByIdWithCountry(int id);
        Task<List<DestinationCity>> GetAllPaginatedDatas(int page, int take = 4);
        Task<int> GetCount();
    }
}

